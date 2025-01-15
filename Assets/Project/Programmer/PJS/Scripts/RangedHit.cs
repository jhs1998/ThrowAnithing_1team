using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedHit : MonoBehaviour
{
    [SerializeField] List<AudioClip> trashHitClips;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == Tag.Trash)
        {
            SoundManager.PlaySFX(trashHitClips[Random.Range(0, trashHitClips.Count)]);
        }
    }
}
