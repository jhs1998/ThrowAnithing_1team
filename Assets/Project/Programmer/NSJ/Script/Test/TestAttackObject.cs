using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttackObject : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == Tag.Player)
        {
            IBattle battle = other.GetComponent<IBattle>();
            battle.TakeDamage(2, CrowdControlType.Stiff, true);
        }
    }
}
