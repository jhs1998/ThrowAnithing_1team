using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "AdditionalEffect/Hit/Electric Shock")]
public class ElectricShockAdditonal : HitAdditional
{
    [Range(0, 100)]
    [SerializeField] private float _moveSpeedReduction;
    [Range(0, 100)]
    [SerializeField] private float _attackSpeedReduction;

    private float _decreasedMoveSpeed;
    private float _decreasedAttackSpeed;
    public override void Enter()
    {
        Debug.Log($"{gameObject.name} 감전");

        // 깎인 이동속도 계산
        float originMoveSpeed = Battle.Debuff.MoveSpeed;
        Battle.Debuff.MoveSpeed *=  1 - _moveSpeedReduction / 100; 
        _decreasedMoveSpeed = originMoveSpeed - Battle.Debuff.MoveSpeed;

        // 깎인 공격속도 계산
        float originAttackSpeed = Battle.Debuff.AttackSpeed;
        Battle.Debuff.AttackSpeed *= 1 - _attackSpeedReduction / 100;
        _decreasedAttackSpeed = originAttackSpeed - Battle.Debuff.AttackSpeed;
        

        if(_debuffRoutine == null)
        {
            _debuffRoutine = CoroutineHandler.StartRoutine(DurationRoutine());
        }
    }
    public override void Exit()
    {
        // 깎인 양 만큼 복구
        Battle.Debuff.MoveSpeed += _decreasedMoveSpeed;
        Battle.Debuff.AttackSpeed += _decreasedAttackSpeed;

        if (_debuffRoutine != null) 
        {
            CoroutineHandler.StopRoutine(_debuffRoutine);
            _debuffRoutine = null;
        }
    }

    IEnumerator DurationRoutine()
    {
        RemainDuraiton = Duration;
        while(RemainDuraiton > 0)
        {
            RemainDuraiton -= Time.deltaTime;
            yield return null;
        }
        Battle.EndDebuff(this);
    }
}
