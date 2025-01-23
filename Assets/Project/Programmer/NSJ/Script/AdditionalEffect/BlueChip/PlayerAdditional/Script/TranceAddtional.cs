using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trance", menuName = "AdditionalEffect/Player/Trance")]
public class TranceAddtional : PlayerAdditional
{
    [Header("������ ���ҷ� (%)")]
    [SerializeField] float DecreaseDamage;
    [Header("���ݼӵ� ������ (%)")]
    [SerializeField] float IncreaseAttackSpeed;
    public override void Enter()
    {
        Model.DamageMultiplier -= DecreaseDamage;
        Model.AttackSpeedMultiplier += IncreaseAttackSpeed;
    }
    public override void Exit()
    {
        Model.DamageMultiplier += DecreaseDamage;
        Model.AttackSpeedMultiplier -= IncreaseAttackSpeed;
    }
}
