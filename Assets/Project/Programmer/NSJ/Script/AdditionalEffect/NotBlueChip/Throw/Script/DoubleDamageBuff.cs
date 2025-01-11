using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleDamage", menuName = "AdditionalEffect/PrevThrow/DoubleDamage")]
public class DoubleDamageBuff : ThrowAdditional
{
    public float DamageMultyplier;



    public override void Enter()
    {
        _throwObject.DamageMultyPlier += DamageMultyplier;
    }
}
