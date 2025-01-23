using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleDamage", menuName = "AdditionalEffect/PrevThrow/DoubleDamage")]
public class DoubleDamageBuff : ThrowAdditional
{
    public float DamageMultyplier;



    public override void Trigger()
    {
        Debug.Log($" ������ ���� ��{_throwObject.DamageMultyPlier}");
        _throwObject.DamageMultyPlier += DamageMultyplier;
        Debug.Log($" ������ �� ��{_throwObject.DamageMultyPlier}");
    }
}
