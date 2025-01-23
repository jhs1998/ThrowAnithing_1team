using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlassCannon", menuName = "AdditionalEffect/Player/GlassCannon")]
public class GlassCannonAdditional : PlayerAdditional
{
    [Header("���ݷ� ������")]
    [SerializeField] private int _increaseDamage;
    [Header("ü�� ���ҷ�(%)")]
    [Range(-100, 0)]
    [SerializeField] private float _decreaseMaxHp;

    public override void Enter()
    {
        Model.AttackPower += _increaseDamage;
        Model.MaxHpMultiplier += _decreaseMaxHp;
    }
    public override void Exit() 
    {
        Model.AttackPower -= _increaseDamage;
        Model.MaxHpMultiplier -= _decreaseMaxHp;
    }
}
