using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slow", menuName = "AdditionalEffect/Hit/Slow")]
public class SlowAddtional : HitAdditional
{
    [System.Serializable]
    struct EffectStrcut
    {
        public GameObject EffectPrefab;
        [HideInInspector] public GameObject Effect;
    }
    [Header("이속 감소량(%)")]
    [SerializeField] public float SlowAmount;
    [SerializeField] EffectStrcut _effect;


    private float _decreaseMoveSpeedEnemyValue;
    public override void Enter()
    {
        if (_debuffRoutine == null)
        {
            _debuffRoutine = CoroutineHandler.StartRoutine(DurationRoutine());
            CreateEffect();
            ChangeValue(true);
        }
    }
    public override void Exit()
    {
        if (_debuffRoutine != null)
        {
            CoroutineHandler.StopRoutine(_debuffRoutine);
            _debuffRoutine = null;
            // 깎인 양 만큼 복구
            ChangeValue(false);
            Destroy(_effect.Effect);
        }
    }

    IEnumerator DurationRoutine()
    {
        _remainDuration = Duration;
        while (_remainDuration > 0)
        {
            _remainDuration -= Time.deltaTime;
            yield return null;
        }
        Battle.EndDebuff(this);
    }

    private void ChangeValue(bool isDecrease)
    {
        if (_targetType == TargetType.Player)
            ChangePlayerValue(isDecrease);
        else if (_targetType == TargetType.Enemy)
            ChangeEnemyValue(isDecrease);
    }

    private void ChangePlayerValue(bool isDecrease)
    {
        // 플레이어 스텟 감소 계산
        if (isDecrease == true)
        {
            Player.Model.MoveSpeedMultyplier -= SlowAmount;
        }
        // 플레이어 스텟 복구
        else
        {
            Player.Model.MoveSpeedMultyplier += SlowAmount;
        }
    }

    private void ChangeEnemyValue(bool isDecrease)
    {
        // 몬스터 스텟 감소 계산
        if (isDecrease == true)
        {
            // 이속감소
            float enemyMoveSpeed = GetEnemyMoveSpeed();
            Debug.Log(enemyMoveSpeed);
            _decreaseMoveSpeedEnemyValue = enemyMoveSpeed * SlowAmount / 100f;
            enemyMoveSpeed -= _decreaseMoveSpeedEnemyValue;
            SetEnemyMoveSpeed(enemyMoveSpeed);

        }
        // 몬스터 스텟 복구
        else
        {
            // 이속 복구
            float enemyMoveSpeed = GetEnemyMoveSpeed();
            enemyMoveSpeed += _decreaseMoveSpeedEnemyValue;
            SetEnemyMoveSpeed(enemyMoveSpeed);
        }
    }


    private void CreateEffect()
    {
        _effect.Effect = Instantiate(_effect.EffectPrefab, transform);
        _effect.Effect.transform.position = transform.position;
    }
}
