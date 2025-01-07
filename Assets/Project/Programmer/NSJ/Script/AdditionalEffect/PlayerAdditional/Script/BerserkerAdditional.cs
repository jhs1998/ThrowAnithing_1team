using UnityEngine;

[CreateAssetMenu(fileName = "Berserker", menuName = "AdditionalEffect/Player/Berserker")]
public class BerserkerAdditional : PlayerAdditional
{
    [Header("공격력 증가량(%)")]
    [SerializeField] private float _increaseDamage;
    [Header("공격 당 체력 감소 량(%)")]
    [SerializeField] private float _hitAmount;

    private int _increaseDamageAmount;

    public override void Enter()
    {
        _increaseDamageAmount = (int)(Model.AttackPower * _increaseDamage / 100);
        Model.AttackPower = GetPlayerAttackPower(_increaseDamageAmount);
    }
    public override void Exit()
    {
        Model.AttackPower = GetPlayerAttackPower(-_increaseDamageAmount);
    }

    public override void Trigger()
    {
        if (CurState != PlayerController.State.ThrowAttack
            || CurState != PlayerController.State.MeleeAttack
            || CurState != PlayerController.State.SpecialAttack)
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        int damage = (int)(Model.MaxHp * _hitAmount / 100f);
        Player.Battle.TargetAttack(Player.transform, damage, false);
    }
}
