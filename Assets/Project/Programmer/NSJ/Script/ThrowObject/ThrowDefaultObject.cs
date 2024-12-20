using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDefaultObject : ThrowObject
{
    protected override void OnCollisionEnter(Collision collision)
    {
        if (_canAttack == true)
        {
            if (collision.gameObject.layer == 4)
            {
                HitTarget();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
