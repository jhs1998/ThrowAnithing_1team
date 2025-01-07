using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Adrenaline", menuName = "AdditionalEffect/Player/Adrenaline")]
public class AdrenalineAddtional : PlayerAdditional
{
    [Header("공격력 증가량")]
    [SerializeField] private int _increaseDamage;
    [Header("최대 스택")]
    [SerializeField] private int _maxCount;

    private int _curCount;
    public override void Enter()
    {
        Player.OnThrowObjectResult += CheckHitEnemy;
    }
    public override void Exit()
    {
        Player.OnThrowObjectResult -= CheckHitEnemy;
        ResetCount();
    }

    private void CheckHitEnemy(bool success)
    {
        if(success == true)
        {
            CountUp();
        }
        else
        {
            ResetCount();
        }
    }

    private void CountUp()
    {
        // 최대 스택 초과 불가
        if (_curCount >= _maxCount)
            return;

        // 스택 증가
        _curCount++;
        Model.AttackPower = GetPlayerAttackPower(_increaseDamage);
    }

    private void ResetCount()
    {
        Model.AttackPower = GetPlayerAttackPower(-(_increaseDamage * _curCount));
        _curCount = 0;
    }
}
