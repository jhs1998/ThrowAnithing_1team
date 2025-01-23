using EPOOutline;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outlinable))]
[RequireComponent(typeof(ThrowObjectOutline))]
public class ThrowObject : MonoBehaviour
{
    [System.Serializable]
    protected partial struct EffectStruct
    {
        public GameObject Hit;
        public GameObject BoomHit;
    }
    // ����Ʈ
    [SerializeField] protected EffectStruct Effect;


    public ThrowObjectData Data;
    public bool CanAttack;
    [SerializeField] public List<ThrowAdditional> ThrowAdditionals = new List<ThrowAdditional>();

    // ������Ʈ ��ü ������
    public int ObjectDamage;
    // �÷��̾��� �߰� ������
    [HideInInspector] public int PlayerDamage;
    public int Damage => ObjectDamage + PlayerDamage;
    // ������ ���
    public float DamageMultyPlier;
    [Space(10)]
    [HideInInspector] public bool IsBoom;
    // ���� ����(���߽�)
    [HideInInspector] public float Radius;
    // CC�� ����
    [HideInInspector] public CrowdControlType CCType;
    // �˹�Ÿ�
    [HideInInspector] public float KnockBackDistance;
    // ���׹̳� ȸ����
    [HideInInspector] public float SpecialRecovery;

    [Tooltip("Ŭ�������� ��ô�� ����?")]
    public bool IsClone;
    [Tooltip("���ο��Լ� �Ļ��� ��� ��ô��(ü��)")]
    public List<ThrowObject> ChainList;
    // ü�εȰ��߿� �ϳ��� �¾Ҵ���?
    public bool IsChainHit;

    public List<GameObject> IgnoreTargets = new List<GameObject>();
    protected Collider[] _overlapCollider = new Collider[20];
    protected PlayerController Player;
    protected BattleSystem Battle => Player.Battle;



    [HideInInspector] public Rigidbody Rb;
    protected Collider _collider;

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        Player = GameObject.FindGameObjectWithTag(Tag.Player).GetComponent<PlayerController>();
        gameObject.layer = Layer.ThrowObject;

    }

    protected virtual void Start()
    {

    }
    protected virtual void OnEnable()
    {
        Rb.velocity = Vector3.zero;
        Rb.angularVelocity = Vector3.zero;
        IgnoreTargets.Clear();
        ChainList.Clear();
        CanAttack = true;
        _collider.isTrigger = true;
    }
    protected virtual void OnDisable()
    {
        ClearThrowAddtional();
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (IsClone == true)
        {
            Destroy(gameObject);
        }

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
            // �����ؾ��ϴ� Ÿ�ٴ���� �������� ����
            if (IgnoreTargets.Contains(other.gameObject) == true)
                return;

            TriggerThrowAddtional();
            HitTarget(other);
            Player.ThrowObjectResultCallback(this, true);
            SetChainHit(true);

            RemoveChainList(this);
        }
        else if (tag != Tag.Player)
        {
            CanAttack = false;
            _collider.isTrigger = false;
            Player.ThrowObjectResultCallback(this, false);

            RemoveChainList(this);
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
    public void Init(PlayerController player, CrowdControlType CCType, bool isBoom,List<ThrowAdditional> throwAdditionals)
    {
        Player = player;
        Radius = player.Model.BoomRadius;
        IsBoom = isBoom;

        this.CCType = CCType;
        // ���߽� ȸ�� ������
        SpecialRecovery = player.Model.RegainMana[player.Model.ChargeStep];
        SpecialRecovery += SpecialRecovery * player.Model.RegainAdditiveMana / 100;

        AddThrowAdditional(throwAdditionals, player);
    }
    public void Init(PlayerController player, CrowdControlType CCType,  bool isBoom,int addionalDamage,List<ThrowAdditional> throwAdditionals)
    {
        Player = player;
        PlayerDamage = addionalDamage;
        Radius = player.Model.BoomRadius;
        IsBoom = isBoom;

        this.CCType = CCType;
        // ���߽� ȸ�� ������
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
    /// Ÿ�� ����
    /// </summary>
    protected virtual void HitTarget(Collider other)
    {
        if (CanAttack == false)
            return;

        if(IsBoom== true)
        {
            PowerHitTarget(other);
        }
        else
        {
            BasicHitTarget(other);
        }
    }


    private void PowerHitTarget(Collider other)
    {

        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, Radius, Player.OverLapColliders, 1 << Layer.Monster);

        for (int i = 0; i < hitCount; i++)
        {
            int finalDamage = Player.GetFinalDamage(Damage, DamageMultyPlier, out bool isCritical);
            // ����� �ֱ�
            int hitDamage = Player.Battle.TargetAttackWithDebuff(Player.OverLapColliders[i], isCritical, finalDamage, false);

            Player.Battle.TargetCrowdControl(Player.OverLapColliders[i], CCType);

            if (KnockBackDistance > 0)
                Player.DoKnockBack(Player.OverLapColliders[i].transform, transform.forward, KnockBackDistance);

            // ����
            SoundManager.PlaySFX(isCritical == true ? Player.Sound.Hit.Critical : Player.Sound.Hit.Hit);
        }

        // �÷��̾� Ư������ �ڿ� ȹ��
        Player.Model.CurMana += SpecialRecovery;
        // ����Ʈ 

        ObjectPool.GetPool(Effect.BoomHit, transform.position, transform.rotation, 1.5f);

        Destroy(gameObject);
    }

    private void BasicHitTarget(Collider other)
    {
        int finalDamage = Player.GetFinalDamage(Damage, DamageMultyPlier, out bool isCritical);
        // ����� �ֱ�
        int hitDamage = Player.Battle.TargetAttackWithDebuff(other, isCritical, finalDamage, false);

        Player.Battle.TargetCrowdControl(other, CCType);

        if (KnockBackDistance > 0)
            Player.DoKnockBack(other.transform, transform.forward, KnockBackDistance);

        // �÷��̾� Ư������ �ڿ� ȹ��
        Player.Model.CurMana += SpecialRecovery;
        // ����Ʈ 
        ObjectPool.GetPool(Effect.Hit, transform.position, transform.rotation, 1.5f);

        // ����
        SoundManager.PlaySFX(isCritical == true ? Player.Sound.Hit.Critical : Player.Sound.Hit.Hit);

        Destroy(gameObject);
    }



    /// <summary>
    /// ���� �߰� ȿ�� �ߵ�
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
        EnterThrowAdditional();
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

    public void AddChainList(ThrowObject other)
    {
        if (ChainList.Count == 0)
        {
            ChainList.Add(this);
        }
        // ü�� ����Ʈ�� ��ô�� �߰�
        ChainList.Add(other);
        other.ChainList = ChainList;
    }
    private void SetChainHit(bool hitSuccess)
    {
        foreach (ThrowObject chainObject in ChainList)
        {
            chainObject.IsChainHit = hitSuccess;
        }
    }
    private void RemoveChainList(ThrowObject throwObject)
    {
        if (ChainList.Contains(throwObject))
            ChainList.Remove(throwObject);
    }
}

[System.Serializable]
public class ThrowObjectData
{
    public int ID;
    public string Name;
}
