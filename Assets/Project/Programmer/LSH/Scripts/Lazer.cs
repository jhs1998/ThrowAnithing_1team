using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Lazer : MonoBehaviour
{

    public Coroutine damageRoutine;

     WaitForSeconds wait;
    [Range(1f, 10f)][SerializeField] float timer;

    [SerializeField] int lazerDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.Player)
        {
            IHit hit = other.GetComponent<IHit>();
            //hit.TakeDamage(spikeDamage, true);
            damageRoutine = StartCoroutine(DamageRoutine(hit));
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


    IEnumerator DamageRoutine(IHit hit)
    {
        while (true)
        {
            hit.TakeDamage(lazerDamage, false);
            yield return timer.GetDelay();
        }
    }







}
