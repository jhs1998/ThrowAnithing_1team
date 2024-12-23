using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCodeData : MonoBehaviour, IHit
{
    [SerializeField] int atk;

    public int Atk { get { return atk; } }

    public void TakeDamage(int damage)
    {
        Debug.Log($"{gameObject} ÀÇ TakeDamage");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Monster"))
        {
            IHit hit = other.transform.GetComponent<IHit>();
            hit.TakeDamage(atk);
        }
    }
}
