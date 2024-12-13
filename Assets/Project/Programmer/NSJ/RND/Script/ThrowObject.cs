using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    private Rigidbody _rb;
    private int _damage;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 4)
        {
           
            IHit hit = collision.gameObject.GetComponent<IHit>();
            Attack(hit);
        }
    }

    public void Shoot()
    {
       _rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
    }

    private void Attack(IHit hit)
    {
        hit.TakeDamage(_damage);
        Destroy(gameObject);
    }
}
