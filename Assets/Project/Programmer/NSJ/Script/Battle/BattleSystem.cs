using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CrowdControlType { None, Stiff, Stun, Size}
public class BattleSystem : MonoBehaviour, IBattle
{
    public IHit Hit { get; set; }

    public Transform HitPoint;
    public List<HitAdditional> HitAdditionalList;
    public List<HitAdditional> DebuffList;
    public event UnityAction<int, bool> OnTargetAttackEvent;
    public event UnityAction<int, bool> OnTakeDamageEvent;
    public event UnityAction OnDieEvent;


    [HideInInspector]public bool IsDie;
    private void Awake()
    {
        Hit = GetComponent<IHit>();

        if (HitPoint == null)
        {
            HitPoint = new GameObject("HitTextPoint").transform;
            HitPoint.SetParent(transform, true);
            HitPoint.localPosition = new Vector3(0, 1f, 0);
        }
    }

    private void OnDisable()
    {
        ClearDebuff();
    }
    #region ���� �޼���
    /// <summary>
    /// ���� ��� ������� �ִ� ����
    /// </summary>
    public void TargetDebuff<T>(T target) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        if (battle == null)
            return ;
        battle.TakeDebuff(HitAdditionalList);
    }
    /// <summary>
    /// Ư�� ������� �ִ� ����
    /// </summary>
    public void TargetDebuff<T>(T target, HitAdditional debuff) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        if (battle == null)
            return ;
        battle.TakeDebuff(debuff);
    }
    /// <summary>
    /// Ư�� ������鸸 �ִ� ����
    /// </summary>
    public void TargetDebuff<T>(T target, List<HitAdditional> debuffs) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        if (battle == null)
            return;
        battle.TakeDebuff(debuffs);
    }
    /// <summary>
    /// ������ CC�� 
    /// </summary>
    public void TargetCrowdControl<T>(T target, CrowdControlType type) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        if (battle == null)
            return;
        battle.TakeCrowdControl(type ,1f);
    }
    /// <summary>
    /// ������ CC�� 
    /// </summary>
    public void TargetCrowdControl<T>(T target, CrowdControlType type, float time) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        if (battle == null)
            return;
        battle.TakeCrowdControl(type, time);
    }
    /// <summary>
    /// ����� ���ִ� ����
    /// </summary>
    public int TargetAttack<T>(T target, int damage) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        if (battle == null)
            return 0;
        int hitDamage = battle.TakeDamage(false, damage, false); // ��븦 ����
        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// ����� ���ִ� ���� (ġ��Ÿ)
    /// </summary>
    public int TargetAttack<T>(T target, bool isCritical, int damage) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        if (battle == null)
            return 0;
        int hitDamage = battle.TakeDamage(isCritical, damage, false); // ��븦 ����
        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// ����� ���ִ� ����(��������)
    /// </summary>
    public int TargetAttack<T>(T target, int damage, bool isIgnoreDef) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        if (battle == null)
            return 0;
        int hitDamage = battle.TakeDamage(damage, isIgnoreDef); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// ����� ���ִ� ���� (��������, ġ��Ÿ)
    /// </summary>
    public int TargetAttack<T>(T target, bool isCritical,int damage, bool isIgnoreDef) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        if (battle == null)
            return 0;
        int hitDamage = battle.TakeDamage(isCritical, damage, isIgnoreDef); // ��븦 ����
        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// ���� ��� ����� �ָ鼭 ����
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        if (battle == null)
            return 0;
        int hitDamage = battle.TakeDamageWithDebuff(false, damage, HitAdditionalList, false); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// ���� ��� ����� �ָ鼭 ���� (ġ��Ÿ)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, bool isCritical, int damage) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        if (battle == null)
            return 0;
        int hitDamage = battle.TakeDamageWithDebuff(isCritical, damage, HitAdditionalList, false); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// ���� ��� ����� �ָ鼭 ���� (��������)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isIgnoreDef) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        if (battle == null)
            return 0;
        int hitDamage = battle.TakeDamageWithDebuff(false,damage, HitAdditionalList, isIgnoreDef); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// ���� ��� ����� �ָ鼭 ���� (ġ��Ÿ, ��������)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, bool isCritical, int damage,  bool isIgnoreDef) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        if (battle == null)
            return 0;
        int hitDamage =  battle.TakeDamageWithDebuff(isCritical, damage, HitAdditionalList,isIgnoreDef); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    #endregion
    #region �ǰ� �޼���
    /// <summary>
    /// �ȶ����� Ư�� ������� �ֱ�
    /// </summary>
    public void TakeDebuff(HitAdditional debuff)
    {
        AddDebuff(debuff);
    }
    /// <summary>
    /// �ȶ����� Ư�� ������鸸 �ֱ�
    /// </summary>
    public void TakeDebuff(List<HitAdditional> debuffs)
    {
        // ����� �߰�
        foreach (HitAdditional debuff in debuffs)
        {
            AddDebuff(debuff);
        }
    }
    /// <summary>
    /// CC�� �±�
    /// </summary>
    public void TakeCrowdControl(CrowdControlType type)
    {
        Hit.TakeCrowdControl(type, 1f);
    }
    /// <summary>
    /// CC�� �±�
    /// </summary>
    /// <param name="type"></param>
    public void TakeCrowdControl(CrowdControlType type,float time)
    {
        Hit.TakeCrowdControl(type, time);
    }
    /// <summary>
    /// ����� ���ִ� ���� �±� (Ÿ��, ġ��Ÿ üũ)
    /// </summary>
    public int TakeDamage(int damage)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, false);
        CreateDamageText(hitDamage, false);

        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// ����� ���ִ� ���� �±� (Ÿ��, ġ��Ÿ üũ)
    /// </summary>
    public int TakeDamage(bool isCritical, int damage)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, false);
        CreateDamageText(hitDamage, isCritical);

        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// ����� ���ִ� ���� �±�
    /// </summary>
    public int TakeDamage(int damage, bool isIgnoreDef)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isIgnoreDef);
        CreateDamageText(hitDamage);

        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// ����� ���ִ� ���� �±� (Ÿ��, ġ��Ÿ üũ)
    /// </summary>
    public int TakeDamage(bool isCritical, int damage,  bool isIgnoreDef)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isIgnoreDef);
        CreateDamageText(hitDamage, isCritical);

        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// ���ݹ����鼭 ����� ���� �ޱ� (Ÿ��)
    /// </summary>
    public int TakeDamageWithDebuff(bool isCritical, int damage, List<HitAdditional> debuffs)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, false);
        CreateDamageText(hitDamage, isCritical);
        // ����� �߰�
        foreach (HitAdditional hitAdditional in debuffs)
        {
            AddDebuff(hitAdditional, hitDamage, isCritical);
        }
        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// ���ݹ����鼭 ����� ���� �ޱ�
    /// </summary>
    public int TakeDamageWithDebuff(int damage, List<HitAdditional> debuffs, bool isIgnoreDef)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isIgnoreDef);
        CreateDamageText(hitDamage);
        // ����� �߰�
        foreach (HitAdditional hitAdditional in debuffs) 
        {
            AddDebuff(hitAdditional, hitDamage, false);
        }
        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// ���ݹ����鼭 ����� ���� �ޱ� (Ÿ��)
    /// </summary>
    public int TakeDamageWithDebuff(bool isCritical, int damage, List<HitAdditional> debuffs, bool isIgnoreDef)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isIgnoreDef);
        CreateDamageText(hitDamage, isCritical);
        // ����� �߰�
        foreach (HitAdditional hitAdditional in debuffs)
        {
            AddDebuff(hitAdditional, hitDamage, isCritical);
        }
        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    #endregion
    #region ������ UI 
    /// <summary>
    /// ������ UI ����
    /// </summary>
    private void CreateDamageText(int damage)
    {
        if (damage <= 0)
            return;
        DamageText text = Instantiate(DataContainer.GetDamageText(CrowdControlType.None), transform.position, Quaternion.identity);
        bool isPlayer = gameObject.tag == Tag.Player;
        text.SetDamageText(damage, HitPoint, isPlayer);
    }
    /// <summary>
    /// ������ UI ����
    /// </summary>
    private void CreateDamageText(int damage, bool isCritical)
    {
        if (damage <= 0)
            return;
        DamageText text = Instantiate(DataContainer.GetDamageText(CrowdControlType.None), transform.position, Quaternion.identity);
        bool isPlayer = gameObject.tag == Tag.Player;
        text.SetDamageText(damage, HitPoint, CrowdControlType.None, isCritical, isPlayer);
    }
    /// <summary>
    /// ������ UI ����
    /// </summary>
    private void CreateDamageText(int damage, CrowdControlType type)
    {
        if (damage <= 0)
            return;

        DamageText text = Instantiate(DataContainer.GetDamageText(CrowdControlType.None), transform.position, Quaternion.identity);
        bool isPlayer = gameObject.tag == Tag.Player;
        text.SetDamageText(damage, HitPoint, type, false, isPlayer);
    }
    /// <summary>
    /// ������ UI ����
    /// </summary>
    private void CreateDamageText(int damage, CrowdControlType type, bool isCritical)
    {
        if (damage <= 0)
            return;

        DamageText text = Instantiate(DataContainer.GetDamageText(CrowdControlType.None), transform.position, Quaternion.identity);
        bool isPlayer = gameObject.tag == Tag.Player;
        text.SetDamageText(damage, HitPoint, type, isCritical, isPlayer);
    }
    #endregion
    #region ȿ�� ���
    /// <summary>
    /// ���� ȿ�� ���
    /// </summary>
    /// <param name="hitAdditional"></param>
    public void AddHitAdditionalList(HitAdditional hitAdditional)
    {
        // �ߺ�üũ
        int index = HitAdditionalList.FindIndex(origin => origin.Origin.Equals(hitAdditional.Origin));
        // �ߺ��� ��Ͼ���
        if (index != -1)
            return;
        HitAdditionalList.Add(hitAdditional);
    }
    /// <summary>
    /// ���� ȿ�� ����
    /// </summary>
    /// <param name="hitAdditional"></param>
    public void RemoveHitAdditionalList(HitAdditional hitAdditional)
    {
        // �ߺ�üũ
        int index = HitAdditionalList.FindIndex(origin => origin.Origin.Equals(hitAdditional.Origin));
        // �ߺ� ���� �� ���� ����
        if (index == -1)
            return;
        HitAdditionalList.Remove(hitAdditional);
    }
    #endregion
    #region ����� �߰�/����
    /// <summary>
    /// ����� �߰�
    /// </summary>
    /// <param name="debuff"></param>
    private void AddDebuff(HitAdditional debuff)
    {
        if (IsDie == true)
            return;

        int index = DebuffList.FindIndex(origin => origin.Origin.Equals(debuff.Origin));
        if (index >= DebuffList.Count)
            return;
        // ����� �ߺ� ��
        if (index != -1)
        {
            // ���� ����� ���ӽð� ����
            DebuffList[index].Init(0, false, DebuffList[index].Duration);
            // ����� �� �ߵ�
            DebuffList[index].Enter();
        }
        else
        {
            HitAdditional cloneDebuff = Instantiate(debuff);
            // ����� �߰� �� �ߵ�
            DebuffList.Add(cloneDebuff);
            cloneDebuff.Origin = debuff.Origin;
            cloneDebuff.Battle = this;
            cloneDebuff.Init(0, false, cloneDebuff.Duration);
            cloneDebuff.Enter(); // ����� �ߵ�
        }
    }
    /// <summary>
    /// ����� �߰�
    /// </summary>
    /// <param name="debuff"></param>
    private void AddDebuff(HitAdditional debuff, int damage, bool isCritical)
    {
        if (IsDie == true)
            return;

        int index = DebuffList.FindIndex(origin => origin.Origin.Equals(debuff.Origin));
        if (index >= DebuffList.Count)
            return;
        // ����� �ߺ� ��
        if (index != -1)
        {
            // ���� ����� ���ӽð� ����
            DebuffList[index].Init(damage, isCritical, DebuffList[index].Duration);
            // ����� �� �ߵ�
            DebuffList[index].Enter();
        }
        else
        {
            HitAdditional cloneDebuff = Instantiate(debuff);
            // ����� �߰� �� �ߵ�
            DebuffList.Add(cloneDebuff);
            cloneDebuff.Origin = debuff.Origin;
            cloneDebuff.Battle = this;
            cloneDebuff.Init(damage, isCritical, cloneDebuff.Duration);
            cloneDebuff.Enter(); // ����� �ߵ�
        }
    }
    /// <summary>
    /// ����� ����
    /// </summary>
    /// <param name="debuff"></param>
    private void RemoveDebuff(HitAdditional debuff)
    {
        debuff.Exit();
        DebuffList.Remove(debuff);
        Destroy(debuff);
    }
    /// <summary>
    /// ����� ��� ����(����)
    /// </summary>
    private void ClearDebuff()
    {
        foreach (HitAdditional debuff in DebuffList)
        {
            debuff.Exit();
            Destroy(debuff);
        }
        DebuffList.Clear();
    }
    #endregion
    #region �ݹ�
    public void Enter()
    {
        foreach (HitAdditional debuff in DebuffList) 
        {
            debuff.Enter();
        }
    }

    public void Exit()
    {
        foreach (HitAdditional debuff in DebuffList)
        {
            debuff.Exit();
        }
    }

    public void Update()
    {
        foreach (HitAdditional debuff in DebuffList)
        {
            debuff.Update();
        }
    }

    public void FixedUpdate()
    {
        foreach (HitAdditional debuff in DebuffList)
        {
            debuff.FixedUpdate();
        }
    }

    public void Trigger()
    {
        foreach (HitAdditional debuff in DebuffList)
        {
            debuff.Trigger();
        }
    }
    #endregion
    /// <summary>
    /// ����� ���� ȣ��
    /// </summary>
    public void EndDebuff(HitAdditional debuff)
    {
        RemoveDebuff(debuff);
    }
    public void Die()
    {
        IsDie = true;
        OnDieEvent?.Invoke();
        ClearDebuff();
    }
}
