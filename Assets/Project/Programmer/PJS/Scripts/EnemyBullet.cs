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
        // TODO : 플레이어 바라보는거 좋은 방법 있을 시 교체
        //transform.LookAt(target.position + new Vector3(0, 1, 0));
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
            Destroy(gameObject);
        }

        if (curState == State.Poision)
        {
            Debug.Log("생성");
            Instantiate(poisonPlate, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
