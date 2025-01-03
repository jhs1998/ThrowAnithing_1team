using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public enum State { Basic, Poision}
    [SerializeField] Rigidbody rigid;
    [SerializeField] State curState;

    [HideInInspector]public BattleSystem Battle;
    public GameObject obj;

    public int Atk;
    private float speed;

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
            Destroy(gameObject);
        }

        if (curState == State.Poision)
        {
            Debug.Log("»ý¼º");
            Instantiate(obj, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
