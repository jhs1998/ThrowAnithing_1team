using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour
{

    [SerializeField] int stalactiteDamage;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LSH_Player player = other.GetComponent<LSH_Player>();
            player.TakeDamage(stalactiteDamage);
        }
    }

}
