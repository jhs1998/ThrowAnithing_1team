using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public ThrowObjectData Data;
    public bool CanAttack;
    [SerializeField] public List<ThrowAdditional> ThrowAdditionals = new List<ThrowAdditional>();

    // 오브젝트 자체 데미지
    public int ObjectDamage;
    // 플레이어의 추가 데미지
    [HideInInspector] public int PlayerDamage;
    public int Damage => ObjectDamage + PlayerDamage;
    // 데미지 배수
    [HideInInspector] public float DamageMultyPlier;
    [Space(10)]
    // 공격 범위(폭발식)
    public float Radius;
    // CC기 종류
    public CrowdControlType CCType;
    // 넉백거리
    public float KnockBackDistance;
    // 스테미나 회복량
    public float SpecialRecovery;

    public List<GameObject> IgnoreTargets = new List<GameObject>();
    protected Collider[] _overlapCollider = new Collider[20];
    protected PlayerController Player;
    protected BattleSystem Battle => Player.Battle;

    [HideInInspector] public Rigidbody Rb;
    protected Collider _collider;

    protected void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        Player = GameObject.FindGameObjectWithTag(Tag.Player).GetComponent<PlayerController>();
        gameObject.layer = Layer.ThrowObject;

        CanAttack = true;
    }

    private void Start()
    {
        EnterThrowAdditional();
    }
    private void OnEnable()
    {
        Rb.velocity = Vector3.zero;
        _collider.isTrigger = true;
    }
    protected virtual void OnDisable()
    {
        ClearThrowAddtional();
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == Layer.Player)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.AddThrowObject(this);
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger == true)
            return;

        string tag = other.gameObject.tag;
        int layer = other.gameObject.layer;
        if (layer == Layer.Monster)
        {
            // 무시해야하는 타겟대상은 공격하지 않음
            if (IgnoreTargets.Contains(other.gameObject) == true)
                return;

            TriggerThrowAddtional();
            HitTarget();
            Player.ThrowObjectResultCallback(true);
        }
        else if (tag != Tag.Player)
        {
            CanAttack = false;
            _collider.isTrigger = false;
            Player.ThrowObjectResultCallback(false);
        }
    }

    private void Update()
    {
        UpdateThrowAdditional();

        if (CanAttack == false)
        {
            if (Player.Model.CurThrowables >= Player.Model.MaxThrowables)
            {
                gameObject.layer = Layer.CantPickTrash;
            }
            else
            {
                gameObject.layer = Layer.CanPickTrash;
            }
        }
    }
    private void FixedUpdate()
    {
        FixedUpdateThrowAdditional();
    }
    #region Init
    public void Init(PlayerController player,CrowdControlType CCType, List<ThrowAdditional> throwAdditionals)
    {
        Player = player;
        Radius = player.Model.BoomRadius;

        this.CCType = CCType;
        // 적중시 회복 마나량
        SpecialRecovery = player.Model.RegainMana[player.Model.ChargeStep];
        SpecialRecovery += SpecialRecovery * player.Model.RegainAdditiveMana / 100;

        AddThrowAdditional(throwAdditionals, player);
    }
    public void Init(PlayerController player, CrowdControlType CCType, int addionalDamage, List<ThrowAdditional> throwAdditionals)
    {
        Player = player;
        PlayerDamage = addionalDamage;
        Radius = player.Model.BoomRadius;

        this.CCType = CCType;
        // 적중시 회복 마나량
        SpecialRecovery = player.Model.RegainMana[player.Model.ChargeStep];
        SpecialRecovery += SpecialRecovery * player.Model.RegainAdditiveMana / 100;

        AddThrowAdditional(throwAdditionals, player);
    }
    #endregion
    public void Shoot(float throwPower)
    {
        Rb.AddForce(transform.forward * throwPower, ForceMode.Impulse);
    }
    /// <summary>
    /// 타겟 적중
    /// </summary>
    protected virtual void HitTarget()
    {
        if (CanAttack == false)
            return;

        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, Radius, Player.OverLapColliders, 1 << Layer.Monster);

        for (int i = 0; i < hitCount; i++)
        {
            int finalDamage = Player.GetFinalDamage(Damage, DamageMultyPlier, out bool isCritical);
            // 디버프 주기
            int hitDamage = Player.Battle.TargetAttackWithDebuff(Player.OverLapColliders[i], isCritical, finalDamage, false);

            Player.Battle.TargetCrowdControl(Player.OverLapColliders[i], CCType);

            if (KnockBackDistance > 0)
                Player.DoKnockBack(Player.OverLapColliders[i].transform, transform.forward, KnockBackDistance);
        }

        // 플레이어 특수공격 자원 획득
        Player.Model.CurMana += SpecialRecovery;
        Destroy(gameObject);
    }


    /// <summary>
    /// 공격 추가 효과 발동
    /// </summary>
    public virtual void EnterThrowAdditional()
    {
        foreach (ThrowAdditional throwAdditional in ThrowAdditionals)
        {
            throwAdditional.Enter();
        }
    }
    public virtual void ExitThrowAdditional()
    {
        foreach (ThrowAdditional throwAdditional in ThrowAdditionals)
        {
            throwAdditional.Exit();
        }
    }

    public virtual void UpdateThrowAdditional()
    {
        if (CanAttack == false)
            return;

        foreach (ThrowAdditional throwAdditional in ThrowAdditionals)
        {
            throwAdditional.Update();
        }
    }

    public virtual void FixedUpdateThrowAdditional()
    {
        if (CanAttack == false)
            return;

        foreach (ThrowAdditional throwAdditional in ThrowAdditionals)
        {
            throwAdditional.FixedUpdate();
        }
    }
    public virtual void TriggerThrowAddtional()
    {
        if (CanAttack == false)
            return;

        foreach (ThrowAdditional throwAdditional in ThrowAdditionals)
        {
            throwAdditional.Trigger();
        }
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
    private void RemoveThrowAddtional(ThrowAdditional throwAdditional)
    {
        throwAdditional.Exit();
        ThrowAdditionals.Remove(throwAdditional);
        Destroy(throwAdditional);
    }
    private void ClearThrowAddtional()
    {
        for (int i = ThrowAdditionals.Count - 1; i >= 0; i--)
        {
            RemoveThrowAddtional(ThrowAdditionals[i]);
        }
    }
}

[System.Serializable]
public class ThrowObjectData
{
    public int ID;
    public string Name;
}
