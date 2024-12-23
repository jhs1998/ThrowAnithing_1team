using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour, IHit
{
    [SerializeField] float speed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(x, 0, z);



        transform.Translate(dir.normalized * speed * Time.deltaTime);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            Debug.Log("공격 받음");
        }
    }*/

    public void TakeDamage(int damage)
    {
        Debug.Log($"{damage} 만큼의 피해를 입음");
    }
}
