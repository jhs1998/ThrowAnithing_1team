using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DashCritical", menuName = "AdditionalEffect/Player/DashCritical")]
public class DashCriticalAdditional : PlayerAdditional
{
    [Header("ġ��Ÿ ������(%)")]
    [SerializeField] private float _increaseCritical;
    [Header("ũ��Ƽ�� ���� Ƚ��")]
    [SerializeField] private int _maxCriticalCount;
    private int _criticalCount;
    public override void Enter()
    {
        // ��ô������Ʈ ��� �̺�Ʈ ����
        Player.OnThrowObjectResult += CheckThrowObjectResult;
    }
    public override void Exit()
    {
        // ��ô������Ʈ ��� �̺�Ʈ ���� ����
        Player.OnThrowObjectResult -= CheckThrowObjectResult;
    }

    public override void Trigger()
    {
        // �뽬 ���� �� ġ��Ÿ ����
        if(CurState == PlayerController.State.Dash)
        {
            IncreaseCritical();
        }
        // �������� �� ���� ī��Ʈ ����
        else if (CurState == PlayerController.State.MeleeAttack)
        {
            CheckAttackCount();
        }
        // Ư�� ���ݽ� ���� ī��Ʈ ����
        else if (CurState == PlayerController.State.SpecialAttack)
        {
            CheckAttackCount();
        }
    }

    /// <summary>
    /// ũ��Ƽ�� ����
    /// </summary>
    private void IncreaseCritical()
    {
        if (_criticalCount == 0)
        {
            Model.CriticalChance += _increaseCritical;
        }
        _criticalCount = _maxCriticalCount;
    }

    /// <summary>
    /// ũ��Ƽ�� ����
    /// </summary>
    private void DecreaseCritical()
    {
        Model.CriticalChance -= _increaseCritical;
    }

    private void CheckAttackCount()
    {
        // ����ī��Ʈ 1�� ����, 0�϶� �ٽ� ũ��Ƽ�� Ȯ�� ����ȭ
        if (_criticalCount <= 0)
            return;

        _criticalCount--;
        if(_criticalCount <= 0)
        {
            DecreaseCritical();
        }
    }

    private void CheckThrowObjectResult(ThrowObject throwObject, bool success)
    {
        // ��ô���� ���߽� ����ī��Ʈ ����
        if(success == true)
        {
            CheckAttackCount();
        }
    }
}
