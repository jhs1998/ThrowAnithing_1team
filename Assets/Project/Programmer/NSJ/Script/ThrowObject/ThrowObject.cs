using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public ThrowObjectData Data;

    [SerializeField] public bool CanAttack;
    [SerializeField] protected List<ThrowAdditional> _throwAdditionals = new List<ThrowAdditional>();
    [SerializeField] protected List<HitAdditional> _hitAdditionals = new List<HitAdditional>();
    public Rigidbody Rb;
    protected int _damage;
    protected float _radius;
    protected Collider[] _overlapCollider = new Collider[20];

    protected int _thorwObjectLayer;
    protected int _monsterLayer;
    protected void Awake()
    {
        _thorwObjectLayer = LayerMask.NameToLayer("ThrowObject");
        _monsterLayer = LayerMask.NameToLayer("Monster");

        Rb = GetComponent<Rigidbody>();

        gameObject.layer = _thorwObjectLayer;

        CanAttack = true;
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (CanAttack == true)
        {
            if (collision.gameObject.layer == _monsterLayer)
            {
                HitTarget();
            }
            else if(collision.gameObject.tag != "Player")
            {
                CanAttack = false;
                gameObject.layer = 0;
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

    private void Update()
    {
        UpdateThrowAdditional();
    }
    public void Init(PlayerController player, List<HitAdditional> hitAdditionals, List<ThrowAdditional> throwAdditionals)
    {
        
        _damage += player.Model.Damage;
        _radius = player.Model.BoomRadius;
        AddHitAdditional(hitAdditionals);
        AddThrowAdditional(throwAdditionals,player);
    }

    public void Shoot()
    {
        Rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
    }
    /// <summary>
    /// 공격 추가 효과 발동
    /// </summary>
    public void EnterThrowAdditional()
    {
        foreach(ThrowAdditional throwAdditional in _throwAdditionals)
        {
            throwAdditional.Enter();
        }
    }
    public void ExitThrowAdditional()
    {
        foreach (ThrowAdditional throwAdditional in _throwAdditionals)
        {
            throwAdditional.Exit();
        }
    }

    public void UpdateThrowAdditional()
    {
        if (CanAttack == false)
            return;

        foreach (ThrowAdditional throwAdditional in _throwAdditionals)
        {
            throwAdditional.Update();
        }
    }
    /// <summary>
    /// 타겟 적중
    /// </summary>
    protected void HitTarget()
    {
        if (CanAttack == false)
            return;

        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, _overlapCollider,1<< _monsterLayer);
        if (hitCount > 0)
        {
            for (int i = 0; i < hitCount; i++)
            {
                NSJMonster monster = _overlapCollider[i].gameObject.GetComponent<NSJMonster>();
                // 디버프 주기
                foreach (HitAdditional hitAdditional in _hitAdditionals)
                {
                    hitAdditional.Init(_damage);
                    monster.AddDebuff(hitAdditional);
                }
            }
        }
        DestroyObject();
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }


    protected void AddHitAdditional(List<HitAdditional> hitAdditionals)
    {
        foreach (HitAdditional hitAdditional in hitAdditionals)
        {
            int index = _hitAdditionals.FindIndex(origin => origin.Origin.Equals(hitAdditional.Origin));
            if (index >= _hitAdditionals.Count)
                return;

            if (index == -1)
            {
                HitAdditional isntance = Instantiate(hitAdditional);
                isntance.Origin = hitAdditional.Origin;
                _hitAdditionals.Add(isntance);
            }
        }
    }

    protected void AddThrowAdditional(List<ThrowAdditional> throwAdditionals, PlayerController player)
    {
        foreach(ThrowAdditional throwAdditional in throwAdditionals)
        {
            int index = _throwAdditionals.FindIndex(origin => origin.Origin.Equals(throwAdditional.Origin));
            if (index >= _throwAdditionals.Count)
                return;

            if (index == -1)
            {
                ThrowAdditional instance = Instantiate(throwAdditional);
                instance.Origin = throwAdditional.Origin;
                instance.Init(player, this);
                _throwAdditionals.Add(instance);
            }
        }
    }

    protected void DestroyObject()
    {
        ExitThrowAdditional();              
        Destroy(gameObject); 
    }
}

[System.Serializable]
public class ThrowObjectData
{
    public int ID;
}
