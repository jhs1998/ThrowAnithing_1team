using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public ThrowObjectData Data;

    [SerializeField] public bool CanAttack;
    [SerializeField] public List<ThrowAdditional> ThrowAdditionals = new List<ThrowAdditional>();
    [SerializeField] public List<HitAdditional> HitAdditionals = new List<HitAdditional>();
    public Rigidbody Rb;
    protected int _damage;
    protected float _radius;
    protected Collider[] _overlapCollider = new Collider[20];

    protected void Awake()
    {
        Rb = GetComponent<Rigidbody>();

        gameObject.layer = Layer.ThrowObject;

        CanAttack = true;
    }

    private void Start()
    {
        EnterThrowAdditional();
    }
    private void OnDisable()
    {
        ExitThrowAdditional();
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (CanAttack == true)
        {
            if (collision.gameObject.layer == Layer.Monster)
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
    private void FixedUpdate()
    {
        FixedUpdateThrowAdditional();   
    }

    public void Init(PlayerController player, List<HitAdditional> hitAdditionals, List<ThrowAdditional> throwAdditionals)
    {
        
        _damage += player.Model.Damage;
        _radius = player.Model.BoomRadius;
        AddHitAdditional(hitAdditionals);
        AddThrowAdditional(throwAdditionals,player);
    }

    public void Shoot(float throwPower)
    {
        Rb.AddForce(transform.forward * throwPower, ForceMode.Impulse);
    }
    /// <summary>
    /// 공격 추가 효과 발동
    /// </summary>
    public void EnterThrowAdditional()
    {
        foreach(ThrowAdditional throwAdditional in ThrowAdditionals)
        {
            throwAdditional.Enter();
        }
    }
    public void ExitThrowAdditional()
    {
        foreach (ThrowAdditional throwAdditional in ThrowAdditionals)
        {
            throwAdditional.Exit();
        }
    }

    public void UpdateThrowAdditional()
    {
        if (CanAttack == false)
            return;

        foreach (ThrowAdditional throwAdditional in ThrowAdditionals)
        {
            throwAdditional.Update();
        }
    }

    public void FixedUpdateThrowAdditional()
    {
        if (CanAttack == false)
            return;

        foreach (ThrowAdditional throwAdditional in ThrowAdditionals)
        {
            throwAdditional.FixedUpdate();
        }
    }
    public void TriggerThrowAddtional()
    {
        if (CanAttack == false)
            return;

        foreach (ThrowAdditional throwAdditional in ThrowAdditionals)
        {
            throwAdditional.Trigger();
        }
    }
    public void TriggerFirstThrowAddtional()
    {
        if (CanAttack == false)
            return;

        foreach (ThrowAdditional throwAdditional in ThrowAdditionals)
        {
            throwAdditional.TriggerFirst();
        }
    }
    /// <summary>
    /// 타겟 적중
    /// </summary>
    protected void HitTarget()
    {
        if (CanAttack == false)
            return;

        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, _overlapCollider,1<<Layer.Monster);
        if (hitCount > 0)
        {
            for (int i = 0; i < hitCount; i++)
            {
                NSJMonster monster = _overlapCollider[i].gameObject.GetComponent<NSJMonster>();
                // 디버프 주기
                foreach (HitAdditional hitAdditional in HitAdditionals)
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
            int index = HitAdditionals.FindIndex(origin => origin.Origin.Equals(hitAdditional.Origin));
            if (index >= HitAdditionals.Count)
                return;

            if (index == -1)
            {
                HitAdditional isntance = Instantiate(hitAdditional);
                isntance.Origin = hitAdditional.Origin;
                HitAdditionals.Add(isntance);
            }
        }
    }

    protected void AddThrowAdditional(List<ThrowAdditional> throwAdditionals, PlayerController player)
    {
        foreach(ThrowAdditional throwAdditional in throwAdditionals)
        {
            int index = ThrowAdditionals.FindIndex(origin => origin.Origin.Equals(throwAdditional.Origin));
            if (index >= ThrowAdditionals.Count)
                return;

            if (index == -1)
            {
                ThrowAdditional instance = Instantiate(throwAdditional);
                instance.Origin = throwAdditional.Origin;
                instance.Init(player,throwAdditional ,this);
                ThrowAdditionals.Add(instance);
            }
        }
    }

    protected void DestroyObject()
    {
        //ExitThrowAdditional();              
        Destroy(gameObject); 
    }
}

[System.Serializable]
public class ThrowObjectData
{
    public int ID;
}
