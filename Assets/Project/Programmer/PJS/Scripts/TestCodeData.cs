using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCodeData : MonoBehaviour, IHit
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

    public void TakeDamage(int damage)
    {
        Debug.Log($"{gameObject} ÀÇ TakeDamage");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == Tag.Monster)
        {
            Debug.Log(other.transform);
            IHit hit = other.transform.GetComponent<IHit>();
            hit.TakeDamage(atk);
        }

        gameObject.SetActive(false);
    }
}
