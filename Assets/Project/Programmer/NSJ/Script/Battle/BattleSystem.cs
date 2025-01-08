using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleSystem : MonoBehaviour, IBattle
{
    public IHit Hit { get; set; }
    public IDebuff Debuff { get; set; }

    [SerializeField] private Transform _hitTextPoint;
    public List<HitAdditional> HitAdditionalList;
    public List<HitAdditional> DebuffList;
    public event UnityAction<int, bool> OnTargetAttackEvent;
    public event UnityAction<int, bool> OnTakeDamageEvent;
    public event UnityAction OnDieEvent;


    [HideInInspector]public bool IsDie;
    private void Awake()
    {
        Hit = GetComponent<IHit>();
        Debuff = GetComponent<IDebuff>();

        if (_hitTextPoint == null)
        {
            _hitTextPoint = new GameObject("HitTextPoint").transform;
            _hitTextPoint.SetParent(transform, true);
            _hitTextPoint.localPosition = new Vector3(0, 1f, 0);
        }
    }

    private void OnDisable()
    {
        ClearDebuff();
    }
    #region 공격 메서드
    /// <summary>
    /// 가진 모든 디버프만 주는 공격
    /// </summary>
    public void TargetDebuff<T>(T target) where T : Component
    {
        // 배틀 시스템은 배틀 시스템 끼리 통신 
        // 플레이어 <-> 배틀시스템 <-> 배틀시스템 <->좀비
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDebuff(HitAdditionalList);
    }
    /// <summary>
    /// 특정 디버프만 주는 공격
    /// </summary>
    public void TargetDebuff<T>(T target, HitAdditional debuff) where T : Component
    {
        // 배틀 시스템은 배틀 시스템 끼리 통신 
        // 플레이어 <-> 배틀시스템 <-> 배틀시스템 <->좀비
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDebuff(debuff);
    }
    /// <summary>
    /// 특정 디버프들만 주는 공격
    /// </summary>
    public void TargetDebuff<T>(T target, List<HitAdditional> debuffs) where T : Component
    {
        // 배틀 시스템은 배틀 시스템 끼리 통신 
        // 플레이어 <-> 배틀시스템 <-> 배틀시스템 <->좀비
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDebuff(debuffs);
    }
    /// <summary>
    /// 디버프 안주는 공격
    /// </summary>
    public int TargetAttack<T>(T target, int damage, bool isStun) where T : Component
    {
        // 배틀 시스템은 배틀 시스템 끼리 통신 
        // 플레이어 <-> 배틀시스템 <-> 배틀시스템 <->좀비
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        int hitDamage = battle.TakeDamage(damage, isStun); // 상대를 공격

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// 디버프 안주는 공격 (타입, 치명타)
    /// </summary>
    public int TargetAttack<T>(T target, int damage, bool isStun, bool isCritical) where T : Component
    {
        // 배틀 시스템은 배틀 시스템 끼리 통신 
        // 플레이어 <-> 배틀시스템 <-> 배틀시스템 <->좀비
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        int hitDamage = battle.TakeDamage(damage, isStun, isCritical); // 상대를 공격

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// 디버프 안주는 공격 (타입)
    /// </summary>
    public int TargetAttack<T>(T target, int damage, bool isStun , DamageType type) where T : Component
    {
        // 배틀 시스템은 배틀 시스템 끼리 통신 
        // 플레이어 <-> 배틀시스템 <-> 배틀시스템 <->좀비
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        int hitDamage = battle.TakeDamage(damage, isStun, type); // 상대를 공격

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// 디버프 안주는 공격 (타입, 치명타)
    /// </summary>
    public int TargetAttack<T>(T target, int damage, bool isStun, DamageType type, bool isCritical) where T : Component
    {
        // 배틀 시스템은 배틀 시스템 끼리 통신 
        // 플레이어 <-> 배틀시스템 <-> 배틀시스템 <->좀비
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        int hitDamage = battle.TakeDamage(damage, isStun, type, isCritical); // 상대를 공격

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// 가진 모든 디버프 주면서 공격
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        int hitDamage = battle.TakeDamageWithDebuff(damage, isStun, HitAdditionalList); // 상대를 공격

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// 가진 모든 디버프 주면서 공격 (타입, 치명타)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        int hitDamage =  battle.TakeDamageWithDebuff(damage, isStun, HitAdditionalList, isCritical); // 상대를 공격

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// 가진 모든 디버프 주면서 공격 (타입)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, DamageType type) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        int hitDamage = battle.TakeDamageWithDebuff(damage, isStun, HitAdditionalList, type); // 상대를 공격

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// 가진 모든 디버프 주면서 공격 (타입, 치명타)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, DamageType type, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        int hitDamage = battle.TakeDamageWithDebuff(damage, isStun, HitAdditionalList, type, isCritical);

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// 특정 디버프만 주면서 공격
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, HitAdditional debuff) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        int hitDamage = battle.TakeDamageWithDebuff(damage, isStun, debuff);

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;; // 상대를 공격
    }
    /// <summary>
    /// 특정 디버프만 주면서 공격 (타입, 치명타)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, HitAdditional debuff, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        int hitDamage = battle.TakeDamageWithDebuff(damage, isStun, debuff, isCritical);

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// 특정 디버프만 주면서 공격 (타입)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, HitAdditional debuff, DamageType type) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        int hitDamage = battle.TakeDamageWithDebuff(damage, isStun, debuff, type); // 상대를 공격

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// 특정 디버프만 주면서 공격 (타입, 치명타)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, HitAdditional debuff, DamageType type, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        int hitDamage = battle.TakeDamageWithDebuff(damage, isStun, debuff, type, isCritical); // 상대를 공격

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// 특정 디버프들을 주면서 공격 가능
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, List<HitAdditional> debuffs) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        int hitDamage = battle.TakeDamageWithDebuff(damage, isStun, debuffs); // 상대를 공격

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// 특정 디버프들을 주면서 공격 가능 (타입, 치명타)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, List<HitAdditional> debuffs, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        int hitDamage =battle.TakeDamageWithDebuff(damage, isStun, debuffs, isCritical); // 상대를 공격

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// 특정 디버프들을 주면서 공격 가능 (타입)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, List<HitAdditional> debuffs, DamageType type) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        int hitDamage =battle.TakeDamageWithDebuff(damage, isStun, debuffs, type); // 상대를 공격

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// 특정 디버프들을 주면서 공격 가능 (타입, 치명타)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, List<HitAdditional> debuffs, DamageType type, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        int hitDamage= battle.TakeDamageWithDebuff(damage, isStun, debuffs, type, isCritical); // 상대를 공격

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    #endregion
    #region 피격 메서드
    /// <summary>
    /// 안때리고 특정 디버프만 주기
    /// </summary>
    public void TakeDebuff(HitAdditional debuff)
    {
        AddDebuff(debuff);
    }
    /// <summary>
    /// 안때리고 특정 디버프들만 주기
    /// </summary>
    public void TakeDebuff(List<HitAdditional> debuffs)
    {
        // 디버프 추가
        foreach (HitAdditional debuff in debuffs)
        {
            AddDebuff(debuff);
        }
    }
    /// <summary>
    /// 디버프 안주는 공격 맞기
    /// </summary>
    public int TakeDamage(int damage, bool isStun)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage);

        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// 디버프 안주는 공격 맞기 (타입, 치명타 체크)
    /// </summary>
    public int TakeDamage(int damage, bool isStun, bool isCritical)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, isCritical);

        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// 디버프 안주는 공격 맞기 (타입)
    /// </summary>
    public int TakeDamage(int damage, bool isStun, DamageType type)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage,type);

        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// 디버프 안주는 공격 맞기 (타입, 치명타 체크)
    /// </summary>
    public int TakeDamage(int damage, bool isStun, DamageType type, bool isCritical)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, type, isCritical);

        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// 공격받으면서 디버프 전부 받기
    /// </summary>
    public int TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> debuffs)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage);
        // 디버프 추가
        foreach (HitAdditional hitAdditional in debuffs) 
        {
            AddDebuff(hitAdditional, hitDamage, false);
        }
        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// 공격받으면서 디버프 전부 받기 (타입)
    /// </summary>
    public int TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> debuffs, bool isCritical)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, isCritical);
        // 디버프 추가
        foreach (HitAdditional hitAdditional in debuffs)
        {
            AddDebuff(hitAdditional, hitDamage, isCritical);
        }
        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// 공격받으면서 디버프 전부 받기 (타입)
    /// </summary>
    public int TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> debuffs, DamageType type)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, type);
        // 디버프 추가
        foreach (HitAdditional hitAdditional in debuffs)
        {
            AddDebuff(hitAdditional, hitDamage, false);
        }
        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// 공격받으면서 디버프 전부 받기 (타입, 치명타)
    /// </summary>
    public int TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> debuffs, DamageType type, bool isCritical)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, type, isCritical);
        // 디버프 추가
        foreach (HitAdditional hitAdditional in debuffs)
        {
            AddDebuff(hitAdditional, hitDamage, isCritical);
        }
        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// 특정 디버프만 받기
    /// </summary>
    public int TakeDamageWithDebuff(int damage, bool isStun, HitAdditional debuff)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage);

        AddDebuff(debuff, hitDamage, false);
        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// 특정 디버프만 받기 (치명타)
    /// </summary>
    public int TakeDamageWithDebuff(int damage, bool isStun, HitAdditional debuff, bool isCritical)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, isCritical);

        AddDebuff(debuff, hitDamage, isCritical);
        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// 특정 디버프만 받기 (타입)
    /// </summary>
    public int TakeDamageWithDebuff(int damage, bool isStun, HitAdditional debuff, DamageType type)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage,type);

        AddDebuff(debuff, hitDamage, false);
        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// 특정 디버프만 받기 (타입, 치명타)
    /// </summary>
    public int TakeDamageWithDebuff(int damage, bool isStun, HitAdditional debuff, DamageType type, bool isCritical)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, type, isCritical);

        AddDebuff(debuff, hitDamage, isCritical);
        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    #endregion
    #region 데미지 UI 
    /// <summary>
    /// 데미지 UI 띄우기
    /// </summary>
    private void CreateDamageText(int damage)
    {
        DamageText text = Instantiate(DataContainer.GetDamageText(DamageType.Default), transform.position, Quaternion.identity);
        bool isPlayer = gameObject.tag == Tag.Player;
        text.SetDamageText(damage, _hitTextPoint, isPlayer);
    }
    /// <summary>
    /// 데미지 UI 띄우기
    /// </summary>
    private void CreateDamageText(int damage, bool isCritical)
    {
        DamageText text = Instantiate(DataContainer.GetDamageText(DamageType.Default), transform.position, Quaternion.identity);
        bool isPlayer = gameObject.tag == Tag.Player;
        text.SetDamageText(damage, _hitTextPoint, DamageType.Default, isCritical, isPlayer);
    }
    /// <summary>
    /// 데미지 UI 띄우기
    /// </summary>
    private void CreateDamageText(int damage, DamageType type)
    {
        DamageText text = Instantiate(DataContainer.GetDamageText(DamageType.Default), transform.position, Quaternion.identity);
        bool isPlayer = gameObject.tag == Tag.Player;
        text.SetDamageText(damage, _hitTextPoint, type, false, isPlayer);
    }
    /// <summary>
    /// 데미지 UI 띄우기
    /// </summary>
    private void CreateDamageText(int damage, DamageType type, bool isCritical)
    {
        DamageText text = Instantiate(DataContainer.GetDamageText(DamageType.Default), transform.position, Quaternion.identity);
        bool isPlayer = gameObject.tag == Tag.Player;
        text.SetDamageText(damage, _hitTextPoint, type, isCritical, isPlayer);
    }
    #endregion
    #region 효과 등록
    /// <summary>
    /// 적중 효과 등록
    /// </summary>
    /// <param name="hitAdditional"></param>
    public void AddHitAdditionalList(HitAdditional hitAdditional)
    {
        // 중복체크
        int index = HitAdditionalList.FindIndex(origin => origin.Origin.Equals(hitAdditional.Origin));
        // 중복시 등록안함
        if (index != -1)
            return;
        HitAdditionalList.Add(hitAdditional);
    }
    /// <summary>
    /// 적중 효과 삭제
    /// </summary>
    /// <param name="hitAdditional"></param>
    public void RemoveHitAdditionalList(HitAdditional hitAdditional)
    {
        // 중복체크
        int index = HitAdditionalList.FindIndex(origin => origin.Origin.Equals(hitAdditional.Origin));
        // 중복 없을 시 삭제 안함
        if (index == -1)
            return;
        HitAdditionalList.Remove(hitAdditional);
    }
    #endregion
    #region 디버프 추가/삭제
    /// <summary>
    /// 디버프 추가
    /// </summary>
    /// <param name="debuff"></param>
    private void AddDebuff(HitAdditional debuff)
    {
        if (IsDie == true)
            return;

        int index = DebuffList.FindIndex(origin => origin.Origin.Equals(debuff.Origin));
        if (index >= DebuffList.Count)
            return;
        // 디버프 중복 시
        if (index != -1)
        {
            // 기존 디버프 지속시간 갱신
            DebuffList[index].Init(0, false, DebuffList[index].Duration);
            // 디버프 재 발동
            DebuffList[index].Enter();
        }
        else
        {
            HitAdditional cloneDebuff = Instantiate(debuff);
            // 디버프 추가 후 발동
            DebuffList.Add(cloneDebuff);
            cloneDebuff.Origin = debuff.Origin;
            cloneDebuff.Battle = this;
            cloneDebuff.transform = transform;
            cloneDebuff.Init(0, false, cloneDebuff.Duration);
            cloneDebuff.Enter(); // 디버프 발동
        }
    }
    /// <summary>
    /// 디버프 추가
    /// </summary>
    /// <param name="debuff"></param>
    private void AddDebuff(HitAdditional debuff, int damage, bool isCritical)
    {
        if (IsDie == true)
            return;

        int index = DebuffList.FindIndex(origin => origin.Origin.Equals(debuff.Origin));
        if (index >= DebuffList.Count)
            return;
        // 디버프 중복 시
        if (index != -1)
        {
            // 기존 디버프 지속시간 갱신
            DebuffList[index].Init(damage, isCritical, DebuffList[index].Duration);
            // 디버프 재 발동
            DebuffList[index].Enter();
        }
        else
        {
            HitAdditional cloneDebuff = Instantiate(debuff);
            // 디버프 추가 후 발동
            DebuffList.Add(cloneDebuff);
            cloneDebuff.Origin = debuff.Origin;
            cloneDebuff.Battle = this;
            cloneDebuff.transform = transform;
            cloneDebuff.Init(damage, isCritical, cloneDebuff.Duration);
            cloneDebuff.Enter(); // 디버프 발동
        }
    }
    /// <summary>
    /// 디버프 삭제
    /// </summary>
    /// <param name="debuff"></param>
    private void RemoveDebuff(HitAdditional debuff)
    {
        debuff.Exit();
        DebuffList.Remove(debuff);
        Destroy(debuff);
    }
    /// <summary>
    /// 디버프 모두 삭제(정리)
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
    #region 콜백
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
    /// 디버프 종료 호출
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
