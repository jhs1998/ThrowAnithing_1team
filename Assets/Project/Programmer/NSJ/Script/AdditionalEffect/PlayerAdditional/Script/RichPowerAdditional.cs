using System;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "RichPower", menuName = "AdditionalEffect/Player/RichPower")]
public class RichPowerAdditional : PlayerAdditional
{
    [Header("투척물 당 공격력 증가량(%)")]
    [SerializeField] private float _increaseDamage;

    private int _increaseDamageAmount;
    IDisposable _disposable;
    public override void Enter()
    {
       _disposable = Model.CurThrowCountSubject
            .DistinctUntilChanged()
            .Subscribe(x => { ChangeDamage(x); });

        ChangeDamage(Model.CurThrowables);
    }
    public override void Exit()
    {
        _disposable.Dispose();
        Model.AttackPower -= _increaseDamageAmount;
    }

    private void ChangeDamage(int count)
    {
        // 이 블루칩으로 인해 증가된 공격력 빼기
        Model.AttackPower =  (Model.AttackPower - (int)Model.Data.EquipStatus.Damage) - _increaseDamageAmount;
        // 현재 공격력에서 투척물 한개당 공격력 증가량 계산
        float attackPowerPerObject = (Model.AttackPower * _increaseDamage / 100);
        // 한개당 공격력 * 투척물 갯수만큼 공격력에 추가
        _increaseDamageAmount = (int)(attackPowerPerObject * count);
        Model.AttackPower = (Model.AttackPower - (int)Model.Data.EquipStatus.Damage) + _increaseDamageAmount;
    }
}
