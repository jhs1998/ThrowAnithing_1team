using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour
{

    [SerializeField] int stalactiteDamage;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.Player)
        {
            IHit hit = other.GetComponent<IHit>();
            hit.TakeDamage(stalactiteDamage, true);
        }
    }

}
