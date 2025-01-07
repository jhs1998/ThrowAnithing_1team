using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ExplosionThrow", menuName = "AdditionalEffect/Throw/ExplosionThrow")]
public class ExplosionThrowAdditonal : ThrowAdditional
{
    [SerializeField] private GameObject _expolsionEffect;
    [SerializeField] private float _maxScaleEffectTime;

    [Header("폭발 범위")]
    [SerializeField] private float _explosionRange;
    [Header("폭발 데미지(%)")]
    [SerializeField] private float _explosionDamage;

    private int damage => (int)(_throwObject.Damage * _explosionDamage/ 100);


    public override void Exit()
    {
        Explosion();
    }

    private void Explosion()
    {
        // 가까운 적(이미 맞은 적) 탐지
        TargetInfo nearTarget = FindNearTarget();

        int hitCount = Physics.OverlapSphereNonAlloc(_throwObject.transform.position, _explosionRange, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++) 
        {
            Transform target = Player.OverLapColliders[i].transform;
            // 맞은 적은 또 안때림
            if (target == nearTarget.transform)
                continue;
            Battle.TargetAttack(target, damage, false);
        }

        ShowEffect();
    }

    private void ShowEffect()
    {
        CoroutineHandler.StartRoutine(ShowEffectRoutine());
    }

    IEnumerator ShowEffectRoutine()
    {
        GameObject instance = Instantiate(_expolsionEffect, _throwObject.transform.position, _throwObject.transform.rotation);
        while (true)
        {
            // 이펙트 점점 커짐
            instance.transform.localScale = new Vector3(
              instance.transform.localScale.x + _explosionRange * 2 * Time.deltaTime * (1 / _maxScaleEffectTime),
              instance.transform.localScale.y + _explosionRange * 2 * Time.deltaTime * (1 / _maxScaleEffectTime),
              instance.transform.localScale.z + _explosionRange * 2 * Time.deltaTime * (1 / _maxScaleEffectTime));
            if (instance.transform.localScale.x > _explosionRange * 2)
            {
                break;
            }
            yield return null;
        }

        Destroy(instance);
    }


}
