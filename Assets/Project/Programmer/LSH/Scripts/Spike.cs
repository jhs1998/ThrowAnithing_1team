using System.Collections;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public Coroutine spikeRoutine;

    [Range(0.1f, 1f)][SerializeField] float count;

    [SerializeField] int spikeDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.Player)
        {

            IHit hit = other.GetComponent<IHit>();
            //hit.TakeDamage(spikeDamage, true);
            spikeRoutine = StartCoroutine(SpikeDamageRoutine(hit));
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Tag.Player)
        {
            if (spikeRoutine != null)
            {
                StopCoroutine(spikeRoutine);
            }
        }

    }


    IEnumerator SpikeDamageRoutine(IHit hit)
    {
        while (true)
        {
            hit.TakeDamage(spikeDamage,false, CrowdControlType.Stiff);
            yield return count.GetDelay();
        }
    }
}
