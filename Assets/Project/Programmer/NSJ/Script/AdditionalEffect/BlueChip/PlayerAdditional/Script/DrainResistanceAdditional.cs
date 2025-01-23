using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DrainResistance", menuName = "AdditionalEffect/Player/DrainResistance")]
public class DrainResistanceAdditional : PlayerAdditional
{
    [Header("���� ���ҷ�(%)")]
    [Range(0, 100)][SerializeField] private float _resistanceAmount;

    public override void EnterState()
    {
        if (CurState != PlayerController.State.Drain)
            return;

        Model.DamageReduction += _resistanceAmount;
    }

    public override void ExitState()
    {
        if (CurState != PlayerController.State.Drain)
            return;

        Model.DamageReduction -= _resistanceAmount;
    }
}
