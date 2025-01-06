using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "AttackUp", menuName = "AdditionalEffect/Player/AttackUp")]
public class AttackUp : PlayerAdditional
{
    [Space(10)]
    [SerializeField] private int _damage; 

    public override void Enter()
    {
        Model.AttackPower += _damage;
    }

    public override void Exit()
    {
        Model.AttackPower -= _damage;
    }
}
