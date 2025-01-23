using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Adrenaline", menuName = "AdditionalEffect/Player/Adrenaline")]
public class AdrenalineAddtional : PlayerAdditional
{

    [Header("���ݷ� ������")]
    [SerializeField] private int _increaseDamage;
    [Header("�ִ� ����")]
    [SerializeField] private int _maxCount;

    private int _curCount;
    public override void Enter()
    {
        Player.OnThrowObjectResult += CheckHitEnemy;
    }
    public override void Exit()
    {
        Player.OnThrowObjectResult -= CheckHitEnemy;

        // �� �ٽ� �ʱ�ȭ
        Model.AttackPower = GetPlayerAttackPower(-(_increaseDamage * _curCount));
        _curCount = 0;
    }

    private void CheckHitEnemy(ThrowObject throwObject,bool success)
    {
        if(success == true)
        {
            CountUp();
        }
        else
        {
            ResetCount(throwObject);
        }
    }

    private void CountUp()
    {
        // �ִ� ���� �ʰ� �Ұ�
        if (_curCount >= _maxCount)
            return;

        // ���� ����
        _curCount++;
        Model.AttackPower = GetPlayerAttackPower(_increaseDamage);
    }

    private void ResetCount(ThrowObject throwObject)
    {
        // ü�� ����Ʈ�� �ϳ��̻�(���� ���� �ٸ� ü�ε� ������Ʈ�� ������ ��
        if (throwObject.ChainList.Count > 1)
            return;
        // ü�� ����Ʈ�� �ϳ��� ���ߴµ� �������� ��
        if (throwObject.IsChainHit == true)
            return;

        Model.AttackPower = GetPlayerAttackPower(-(_increaseDamage * _curCount));
        _curCount = 0;
    }
}
