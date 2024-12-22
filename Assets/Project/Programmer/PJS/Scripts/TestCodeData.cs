using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCodeData : MonoBehaviour
{
    [SerializeField] int atk;

    public int Atk { get { return atk; } }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Monster"))
        {
            IHit hit = other.transform.GetComponent<IHit>();
            hit.TakeDamage(atk);
        }
    }
}
