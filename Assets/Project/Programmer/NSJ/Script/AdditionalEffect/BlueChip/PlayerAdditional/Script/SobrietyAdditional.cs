using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Sobriety", menuName = "AdditionalEffect/Player/Sobriety")]
public class SobrietyAdditional : PlayerAdditional
{
    [Header("���� ȸ���� ������(%)")]
    [SerializeField] private float _regainAmount;

    public override void Enter()
    {
        Model.RegainAdditiveMana += _regainAmount;
    }

    public override void Exit()
    {
        Model.RegainAdditiveMana -= _regainAmount;
    }
}
