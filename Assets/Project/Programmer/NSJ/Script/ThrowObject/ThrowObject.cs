using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public ThrowObjectData Data;

    [SerializeField] public bool CanAttack;
    [SerializeField] public List<ThrowAdditional> ThrowAdditionals = new List<ThrowAdditional>();
    public int Damage;
    public float Radius;
    public float KnockBackDistance;
    public float SpecialRecovery;
    protected Collider[] _overlapCollider = new Collider[20];
    protected PlayerController _player;

    [HideInInspector] public Rigidbody Rb;
    protected Collider _collider;

    protected void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        gameObject.layer = Layer.ThrowObject;

        CanAttack = true;
    }

    private void Start()
    {
        EnterThrowAdditional();
    }
    private void OnEnable()
    {
        _collider.isTrigger = true;
    }
    private void OnDisable()
    {
        ExitThrowAdditional();
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == Tag.Player)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.AddThrowObject(this);
        }

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layer.Monster)
        {
            HitTarget();
        }
        else if (other.gameObject.tag != Tag.Player)
        {
            CanAttack = false;
            gameObject.layer = 0;
            _collider.isTrigger = false;
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

    public void Init(PlayerController player, List<ThrowAdditional> throwAdditionals)
    {
        _player = player;
        Damage += player.GetFinalDamage();
        Radius = player.Model.BoomRadius;
        SpecialRecovery = player.Model.RegainMana[player.Model.ChargeStep];
        AddThrowAdditional(throwAdditionals, player);
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
        foreach (ThrowAdditional throwAdditional in ThrowAdditionals)
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

        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, Radius, _player.OverLapColliders, 1 << Layer.Monster);

        for (int i = 0; i < hitCount; i++)
        {

            // 디버프 주기
            _player.Battle.TargetAttackWithDebuff(_player.OverLapColliders[i], Damage, true);

            if (KnockBackDistance > 0)
                _player.DoKnockBack(_player.OverLapColliders[i].transform, transform.forward, KnockBackDistance);
        }

        // 플레이어 특수공격 자원 획득
        _player.Model.CurMana += SpecialRecovery;
        DestroyObject();
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }


    protected void AddThrowAdditional(List<ThrowAdditional> throwAdditionals, PlayerController player)
    {
        foreach (ThrowAdditional throwAdditional in throwAdditionals)
        {
            int index = ThrowAdditionals.FindIndex(origin => origin.Origin.Equals(throwAdditional.Origin));
            if (index >= ThrowAdditionals.Count)
                return;

            if (index == -1)
            {
                ThrowAdditional instance = Instantiate(throwAdditional);
                instance.Origin = throwAdditional.Origin;
                instance.Init(player, throwAdditional, this);
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
