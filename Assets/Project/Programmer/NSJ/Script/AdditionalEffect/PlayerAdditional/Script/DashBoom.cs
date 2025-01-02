using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashBoom", menuName = "AdditionalEffect/Player/DashBoom")]
public class DashBoom : PlayerAdditional
{
    [SerializeField] GameObject _attackEffect;
    [SerializeField] float _range;
    [SerializeField] float _damage;
    [SerializeField] private float _maxScaleEffectTime;
    public override void Trigger()
    {
        if(Player.CurState == PlayerController.State.Dash)
        {
            Attack();
        }
    }

    private void Attack()
    {
        CoroutineHandler.StartRoutine(CreateAttackEffectRoutien());

        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _range, Player.OverLapColliders, 1 << Layer.Monster);
        int finalDamage = Player.GetFinalDamage(_damage);
        for (int i = 0; i < hitCount; i++)
        {
            // µ¥¹ÌÁö ÁÖ±â
            Player.Battle.TargetAttackWithDebuff(Player.OverLapColliders[i], finalDamage, true);
            // ³Ë¹é
            Player.DoKnockBack(Player.OverLapColliders[i].transform, transform, 1f);
        }
    }

    IEnumerator CreateAttackEffectRoutien()
    {
        if (_attackEffect == null)
            yield break;

        GameObject instance = Instantiate(_attackEffect, transform.position, transform.rotation);
        while (true)
        {
            // ÀÌÆåÆ® Á¡Á¡ Ä¿Áü
            instance.transform.localScale = new Vector3(
              instance.transform.localScale.x + _range * 2 * Time.deltaTime * (1 / _maxScaleEffectTime),
              instance.transform.localScale.y + _range * 2 * Time.deltaTime * (1 / _maxScaleEffectTime),
              instance.transform.localScale.z + _range * 2 * Time.deltaTime * (1 / _maxScaleEffectTime));
            if (instance.transform.localScale.x > _range * 2)
            {
                break;
            }
            yield return null;
        }

        Destroy(instance);
    }

    //public override void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, _range);
    //}
}
