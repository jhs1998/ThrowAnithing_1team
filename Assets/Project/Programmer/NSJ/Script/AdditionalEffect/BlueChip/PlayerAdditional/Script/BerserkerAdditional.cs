using UnityEngine;

[CreateAssetMenu(fileName = "Berserker", menuName = "AdditionalEffect/Player/Berserker")]
public class BerserkerAdditional : PlayerAdditional
{

    [Header("���ݷ� ������(%)")]
    [SerializeField] private float _increaseDamage;
    [Header("���� �� ü�� ���� ��(%)")]
    [SerializeField] private float _hitAmount;


    public override void Enter()
    {
        Model.AttackPowerMultiplier += _increaseDamage;
    }
    public override void Exit()
    {
        Model.AttackPowerMultiplier -= _increaseDamage;
    }

    public override void Trigger()
    {
        if (CurState == PlayerController.State.ThrowAttack
            || CurState == PlayerController.State.MeleeAttack
            || CurState == PlayerController.State.SpecialAttack)
        {
            TakeDamage();
        }
            
           
    }

    private void TakeDamage()
    {
        int damage = (int)(Model.MaxHp * _hitAmount / 100f);
        Player.Battle.TargetAttack(Player.transform, damage, true);
    }
}
