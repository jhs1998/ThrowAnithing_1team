using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{

    public Coroutine damageRoutine;

     WaitForSeconds wait;
    [Range(1f, 10f)][SerializeField] float timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LSH_Player player = other.GetComponent<LSH_Player>();

            damageRoutine = StartCoroutine(DamageRoutine(player));
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if(damageRoutine != null)
            StopCoroutine(damageRoutine);
    }

    private void OnDisable()
    {
        if (damageRoutine != null)
            StopCoroutine(damageRoutine);
    }


    IEnumerator DamageRoutine(LSH_Player player)
    {
        wait = new WaitForSeconds(timer);

        while (true)
        {
            yield return wait;
            player.TakeDamage(6);

        }
    }







}
