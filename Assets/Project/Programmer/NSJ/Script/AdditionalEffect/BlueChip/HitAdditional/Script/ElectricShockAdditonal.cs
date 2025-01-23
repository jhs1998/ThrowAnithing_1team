
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "AdditionalEffect/Hit/Electric Shock")]
public class ElectricShockAdditonal : HitAdditional
{
    [Header("Ȯ��")]
    [SerializeField] public float Probability;

    [System.Serializable]
    struct EffectStrcut
    {
        public GameObject EffectPrefab;
        [HideInInspector] public GameObject Effect;
    }
    [Header("�̼Ӱ��ҷ�(%)")]
    [Range(0, 100)]
    [SerializeField] private float _moveSpeedReduction;
    [Header("���� ���ҷ�")]
    [Range(0, 100)]
    [SerializeField] private float _attackSpeedReduction;

    [SerializeField] EffectStrcut _effect;

    private float _decreaseMoveSpeedEnemyValue;
    private float _decreaseAttackSpeedEnemyValue;
    public override void Enter()
    {
        if (Random.Range(0, 100) > Probability)
            return;

        if (_debuffRoutine == null)
        {
            CreateEffect();
            ChangeValue(true);
            _debuffRoutine = CoroutineHandler.StartRoutine(DurationRoutine());
        }
    }
    public override void Exit()
    {
        if (_debuffRoutine != null) 
        {
            CoroutineHandler.StopRoutine(_debuffRoutine);
            _debuffRoutine = null;
            // ���� �� ��ŭ ����
            ChangeValue(false);
            ObjectPool.ReturnPool(_effect.Effect);
        }
    }

    IEnumerator DurationRoutine()
    {
        _remainDuration = Duration;
        while(_remainDuration > 0)
        {
            _remainDuration -= Time.deltaTime;
            yield return null;
        }
        Battle.EndDebuff(this);
    }

    private void ChangeValue(bool isDecrease)
    {
        if( _targetType ==TargetType.Player)
            ChangePlayerValue(isDecrease);
        else if( _targetType ==TargetType.Enemy)
            ChangeEnemyValue(isDecrease);
    }

    private void ChangePlayerValue(bool isDecrease)
    {
        if (transform.tag != Tag.Player)
            return;

        // �÷��̾� ���� ���� ���
        if(isDecrease == true)
        {
            Player.Model.MoveSpeedMultyplier -= _moveSpeedReduction;
            Player.Model.AttackSpeedMultiplier -= _attackSpeedReduction;
        }
        // �÷��̾� ���� ����
        else
        {
            Player.Model.MoveSpeedMultyplier += _moveSpeedReduction;
            Player.Model.AttackSpeedMultiplier += _attackSpeedReduction;
        }
    }

    private void ChangeEnemyValue(bool isDecrease)
    {
        // ���� ���� ���� ���
        if (isDecrease == true)
        {
            // �̼Ӱ���
            float enemyMoveSpeed = GetEnemyMoveSpeed();
            Debug.Log(enemyMoveSpeed);
            _decreaseMoveSpeedEnemyValue = enemyMoveSpeed * _moveSpeedReduction / 100f;
            enemyMoveSpeed -= _decreaseMoveSpeedEnemyValue;
            SetEnemyMoveSpeed(enemyMoveSpeed);

            // ���Ӱ���
            float enemyAttackSpeed = GetEnemyAttackSpeed();
            _decreaseAttackSpeedEnemyValue = enemyAttackSpeed * _attackSpeedReduction / 100f;
            enemyAttackSpeed -= _decreaseAttackSpeedEnemyValue;
            SetEnemyAttackSpeed(enemyAttackSpeed);

        }
        // ���� ���� ����
        else
        {
            // �̼� ����
            float enemyMoveSpeed = GetEnemyMoveSpeed();
            enemyMoveSpeed += _decreaseMoveSpeedEnemyValue;
            SetEnemyMoveSpeed(enemyMoveSpeed);

            // ���� ����
            float enemyAttackSpeed = GetEnemyAttackSpeed();
            enemyAttackSpeed += _decreaseAttackSpeedEnemyValue;
            SetEnemyAttackSpeed(enemyAttackSpeed);

        }
    }

    private void CreateEffect()
    {
        _effect.Effect = ObjectPool.GetPool(_effect.EffectPrefab, transform);
        _effect.Effect.transform.position = Battle.HitPoint.position;
    }
}
