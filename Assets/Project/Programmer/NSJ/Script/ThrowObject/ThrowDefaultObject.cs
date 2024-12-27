using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDefaultObject : ThrowObject
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layer.Monster)
        {
            HitTarget();
        }
        else if (other.gameObject.tag != Tag.Player)
        {
            Destroy(gameObject);
        }
    }
}
