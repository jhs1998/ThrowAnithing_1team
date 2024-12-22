using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDefaultObject : ThrowObject
{
    protected override void OnCollisionEnter(Collision collision)
    {
        if (CanAttack == true)
        {
            if (collision.gameObject.layer == Layer.Monster)
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
