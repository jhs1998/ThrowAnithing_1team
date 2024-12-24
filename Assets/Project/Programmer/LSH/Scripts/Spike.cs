using System.Collections;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public Coroutine spikeRoutine;

    WaitForSeconds DamageTiming;

    [Range(0.1f, 1f)][SerializeField] float count;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            Debug.Log(3);
            LSH_Player player = other.GetComponent<LSH_Player>();

            spikeRoutine = StartCoroutine(SpikeDamageRoutine(player));


        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (spikeRoutine != null)
            {
                StopCoroutine(spikeRoutine);
                Debug.Log(4);
            }
        }

    }


    IEnumerator SpikeDamageRoutine(LSH_Player player)
    {
        DamageTiming = new WaitForSeconds(count);

        while (true)
        {
            player.TakeDamage(10);
            yield return DamageTiming;
        }

    }



}