using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDefaultObject : ThrowObject
{
    protected override void OnCollisionEnter(Collision collision)
    {
        if (CanAttack == true)
        {
            if (collision.gameObject.layer == _monsterLayer)
            {
                HitTarget();
            }
            else
            {
                CanAttack = false;
                DestroyObject();
            }
        }
    }
}
