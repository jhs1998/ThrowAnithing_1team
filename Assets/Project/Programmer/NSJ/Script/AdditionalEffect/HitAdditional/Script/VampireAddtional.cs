using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Vampire", menuName = "AdditionalEffect/Player/Vampire")]
public class VampireAddtional : PlayerAdditional
{
    [Header("ÇÇÇØ ÈíÇ÷·®(%)")]
    [SerializeField] private float _lifeDrainAmount;

    public override void Enter()
    {
        Player.Battle.OnTargetAttackEvent += DrainLife;
    }

    public override void Exit()
    {
        Player.Battle.OnTargetAttackEvent -= DrainLife;
    }

    private void DrainLife(int damage, bool isCritical)
    {
        Debug.Log($"Å©¸®Æ¼ÄÃ? {isCritical}");
        if (isCritical == false)
            return;
        Debug.Log($"ÇÇÈí {damage}");
        Player.DrainLife(damage, _lifeDrainAmount);
    }
}
