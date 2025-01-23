using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield", menuName = "AdditionalEffect/Player/Shield")]
public class ShieldAdditional : PlayerAdditional
{
    [System.Serializable]
    struct EffectStrcut
    {
        public GameObject EffectPrefab;
        [HideInInspector] public GameObject Effect;
    }
    [SerializeField] EffectStrcut _effect;

    [Header("���� ����� �ð�")]
    [SerializeField] private float _regenerateTime;

    private bool _isShield;
    Coroutine _regenerateShieldRoutine;
    public override void Enter()
    {
        Player.OnPlayerHitActionEvent += HitPlayerWithShield;

        _effect.Effect = Instantiate(_effect.EffectPrefab, transform);
        CreateShield();
    }

    public override void Exit()
    {
        Player.OnPlayerHitActionEvent -= HitPlayerWithShield;

        // �ڷ�ƾ ����
        if( _regenerateShieldRoutine != null)
        {
            CoroutineHandler.StopRoutine(_regenerateShieldRoutine);
            _regenerateShieldRoutine = null;
        }
        // ���尡 �־����� ���� �ı�
        if( _isShield == true)
        {
            DestroyShield();
        }
        Destroy(_effect.Effect);
    }

    private void CreateShield()
    {
        Player.IsShield = true;
        _effect.Effect.SetActive(true);
    }
    private void DestroyShield()
    {
        Player.IsShield = false;
        _effect.Effect.SetActive(false);
    }

    private void HitPlayerWithShield()
    {
        if (_regenerateShieldRoutine == null)
            _regenerateShieldRoutine = CoroutineHandler.StartRoutine(RegenerateShieldRoutine());
    }

    IEnumerator RegenerateShieldRoutine()
    {
        // 1������ ���(�ǰ� ���� ���� �޾ƾ���)
        yield return null;
        // ���� �ı�
        DestroyShield();
        // ���� ����� ���
        yield return _regenerateTime.GetDelay();
        CreateShield();
        _regenerateShieldRoutine = null;
    }
}
