using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [HideInInspector] public BattleSystem Battle;
    [HideInInspector] public Transform target;    // 플레이어
    public int Atk;     // 공격력

    private Rigidbody rigid;
    private float speed;    // 투척 스피드

    public float Speed { set { speed = value; } }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigid.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == Tag.Player)
        {
            Battle.TargetAttack(other.transform, Atk, true);
            ObjectPool.ReturnPool(this);
        }

        ObjectPool.ReturnPool(this);
    }
}
