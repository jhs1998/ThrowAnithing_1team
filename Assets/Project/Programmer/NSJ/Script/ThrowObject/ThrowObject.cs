using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public ThrowObjectData Data;

    [SerializeField] private List<HitAdditional> _hitAdditionals = new List<HitAdditional>();
    [SerializeField] private bool _canAttack;
    private Rigidbody _rb;
    private int _damage;
    private float _radius;

    private Collider[] _overlapCollider = new Collider[20];

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _canAttack = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (_canAttack == true)
        {
            if (collision.gameObject.layer == 4)
            {
                HitTarget();
            }
            else if((collision.gameObject.tag != "Player"))
            {
                _canAttack = false;
            }
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                player.AddThrowObject(this);        
            }
        }
    }

    public void Init(int damage,float radius, List<HitAdditional> hitAdditionals)
    {
        _damage = damage;
        _radius = radius;
        AddHitAdditional(hitAdditionals);
    }

    public void Shoot()
    {
        _rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
    }

    private void HitTarget()
    {
        if (_canAttack == false)
            return;

        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, _overlapCollider, 1 << 4);
        if (hitCount > 0)
        {
            for (int i = 0; i < hitCount; i++)
            {
                NSJMonster monster = _overlapCollider[i].gameObject.GetComponent<NSJMonster>();
                // 디버프 주기
                foreach (HitAdditional hitAdditional in _hitAdditionals)
                {
                    HitAdditional cloneDebuff = Instantiate(hitAdditional);
                    cloneDebuff.Origin = hitAdditional;
                    cloneDebuff.Init(_damage);
                    monster.AddDebuff(cloneDebuff);
                }
            }
        }
        Destroy(gameObject);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private void AddHitAdditional(List<HitAdditional> hitAdditionals)
    {
        foreach (HitAdditional hitAdditional in hitAdditionals)
        {
            int index = _hitAdditionals.FindIndex(origin => origin.Origin.Equals(hitAdditional.Origin));
            if (index >= _hitAdditionals.Count)
                return;

            if (index == -1)
            {
                _hitAdditionals.Add(hitAdditional);
            }
        }
    }

}

[System.Serializable]
public class ThrowObjectData
{
    public int ID;
}
