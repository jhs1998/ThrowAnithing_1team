using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public enum State { Basic, Poision}
    [SerializeField] Rigidbody rigid;
    [SerializeField] State curState;

    public GameObject obj;

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
            Debug.Log("충돌");
            Destroy(gameObject);
        }

        if (curState == State.Poision)
        {
            Debug.Log("생성");
            Instantiate(obj, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
