using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExtensionDrain", menuName = "AdditionalEffect/Player/ExtensionDrain")]
public class ExtensionDrainAddtional : PlayerAdditional
{
    [Header("�巹�� ���� ������(%)")]
    [SerializeField] private float _increaseDrainDistance;

    public override void Enter()
    {
        Model.DrainDistanceMultyPlier += _increaseDrainDistance;
    }

    public override void Exit() 
    {
        Model.DrainDistanceMultyPlier -= _increaseDrainDistance;
    }

}
