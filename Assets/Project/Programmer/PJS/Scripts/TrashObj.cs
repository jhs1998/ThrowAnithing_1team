using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashObj : MonoBehaviour, IHit
{
    [SerializeField] int atk;

    public int Atk { get { return atk; } }

    [SerializeField] EnemyBullet bulletPref;
    [SerializeField] Transform pos;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(bulletPref, pos);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == Tag.Monster)
        {
            Debug.Log(other.transform);
            IHit hit = other.transform.GetComponentInParent<IHit>();
            hit.TakeDamage(atk, false, CrowdControlType.Stiff);
        }

        //gameObject.SetActive(false);
    }

    public int TakeDamage(int damage, bool isIgnoreDef, CrowdControlType type)
    {
        //Debug.Log($"{gameObject} ÀÇ TakeDamage");
        return damage;
    }
}
