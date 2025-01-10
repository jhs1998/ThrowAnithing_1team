using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public enum State { Basic, Poision }
    [SerializeField] Rigidbody rigid;
    [SerializeField] State curState;

    [HideInInspector] public BattleSystem Battle;
    public GameObject poisonPlate;  // 독 장판
    public Transform target;    // 플레이어
    public int Atk;     // 공격력

    private float speed;    // 투척 스피드

    public float Speed { set { speed = value; } }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(DestoryRoutine());
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

        if (curState == State.Poision)
        {
            Debug.Log("생성");
            Instantiate(poisonPlate, transform.position, transform.rotation);
        }
        ObjectPool.ReturnPool(this);
    }

    IEnumerator DestoryRoutine()
    {
        yield return 5f.GetDelay();
        ObjectPool.ReturnPool(this);
    }
}
