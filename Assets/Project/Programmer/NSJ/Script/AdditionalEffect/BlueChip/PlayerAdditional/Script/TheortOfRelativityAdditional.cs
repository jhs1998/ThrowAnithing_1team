using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "TheortOfRelativity", menuName = "AdditionalEffect/Player/TheortOfRelativity")]
public class TheortOfRelativityAdditional : PlayerAdditional
{

    [System.Serializable]
    struct EffectStrcut
    {
        public GameObject EffectPrefab;
        [HideInInspector] public GameObject Effect;
        public float EffectDuration;
    }
    [SerializeField] EffectStrcut _effect;
    [SerializeField] private SlowAddtional _slowOrigin;
    private SlowAddtional _slow;
    [Header("Ȯ��(%)")]
    [Range(0, 100)]
    [SerializeField] private float _probability;
    [Header("���ݼӵ� ������(%)")]
    [SerializeField] private float _increaseAttackSpeed;
    [Header("�̵��ӵ� ������(%)")]
    [SerializeField] private float _increaseMoveSpeed;
    [Header("���� �ð�")]
    [SerializeField] private float _buffDuration;
    [Header("���ο� ����")]
    [SerializeField] private float _slowRange;
    [Header("�� �̼Ӱ��ҷ�")]
    [SerializeField] private float _slowAmount;
    [Header("����� �ð�")]
    [SerializeField] private float _slowDuration;


    private bool _isBuff;
    Coroutine _buffRoutine;
    public override void Enter()
    {
        _slow = Instantiate(_slowOrigin); 
        _slow.Duration = _slowDuration;
        _slow.SlowAmount = _slowAmount;
    }


    public override void Exit()
    {
        // ���� �ÿ� ���� ����
        if (_buffRoutine != null)
        {
            CoroutineHandler.StopRoutine(_buffRoutine);
            _buffRoutine = null;
        }
        if (_isBuff ==true)
        {
            Model.AttackSpeedMultiplier -= _increaseAttackSpeed;
            Model.MoveSpeedMultyplier -= _increaseMoveSpeed;
        }
        Destroy(_slow);
    }

    public override void Trigger()
    {
        if (CurState != PlayerController.State.SpecialAttack)
            return;

        if (Random.Range(0, 100) < _probability)
        {
            Process();
        }
    }

    private void Process()
    {
        ProcessPlayerBuff();
        ProcessEnemyDebuff();
    }

    private void ProcessPlayerBuff()
    {
        if (_buffRoutine != null)
        {
            CoroutineHandler.StopRoutine( _buffRoutine );
            _buffRoutine = null;
        }

        if (_buffRoutine == null)
            _buffRoutine = CoroutineHandler.StartRoutine(BuffRoutine());
    }

    IEnumerator BuffRoutine()
    {
        // ���� �ߺ� ����
        if (_isBuff == false)
        {
            _isBuff = true;

            Model.AttackSpeedMultiplier += _increaseAttackSpeed;
            Model.MoveSpeedMultyplier += _increaseMoveSpeed;
        }

        yield return _buffDuration.GetDelay();
        // �÷��̾� ���� ��
        Model.AttackSpeedMultiplier -= _increaseAttackSpeed;
        Model.MoveSpeedMultyplier -= _increaseMoveSpeed;


        _isBuff = false;
        _buffRoutine = null;
    }

    private void ProcessEnemyDebuff()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _slowRange, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            // TODO: ���ο� ��� �߰�
            Battle.TargetDebuff(Player.OverLapColliders[i], _slow);
        }

        CreateSlowFieldEffect();
    }

    private void CreateSlowFieldEffect()
    {
        _effect.Effect = ObjectPool.GetPool(_effect.EffectPrefab, transform, _effect.EffectDuration);

        Vector3 effectScale = _effect.Effect.transform.localScale;
        _effect.Effect.transform.localScale = new Vector3(_slowRange * 2, effectScale.y, _slowRange * 2);
    }
}
