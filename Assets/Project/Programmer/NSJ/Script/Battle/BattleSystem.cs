using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour, IBattle
{
    public IHit Hit { get; set; }
    public IDebuff Debuff { get; set; }

    [SerializeField] private Transform _hitTextPoint;

    [SerializeField] private List<HitAdditional> _hitAdditionalList;
    [SerializeField] private List<HitAdditional> _debuffList;



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
    #region 공격 메서드
    /// <summary>
    /// 가진 모든 디버프만 주는 공격
    /// </summary>
    public void TargetDebuff<T>(T target) where T : Component
    {
        // 배틀 시스템은 배틀 시스템 끼리 통신 
        // 플레이어 <-> 배틀시스템 <-> 배틀시스템 <->좀비
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDebuff(_hitAdditionalList);
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
    public void TargetAttack<T>(T target, int damage, bool isStun) where T : Component
    {
        // 배틀 시스템은 배틀 시스템 끼리 통신 
        // 플레이어 <-> 배틀시스템 <-> 배틀시스템 <->좀비
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDamage(damage, isStun); // 상대를 공격
    }
    /// <summary>
    /// 디버프 안주는 공격 (타입, 치명타)
    /// </summary>
    public void TargetAttack<T>(T target, int damage, bool isStun, bool isCritical) where T : Component
    {
        // 배틀 시스템은 배틀 시스템 끼리 통신 
        // 플레이어 <-> 배틀시스템 <-> 배틀시스템 <->좀비
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDamage(damage, isStun, isCritical); // 상대를 공격
    }
    /// <summary>
    /// 디버프 안주는 공격 (타입)
    /// </summary>
    public void TargetAttack<T>(T target, int damage, bool isStun , DamageType type) where T : Component
    {
        // 배틀 시스템은 배틀 시스템 끼리 통신 
        // 플레이어 <-> 배틀시스템 <-> 배틀시스템 <->좀비
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDamage(damage, isStun, type); // 상대를 공격
    }
    /// <summary>
    /// 디버프 안주는 공격 (타입, 치명타)
    /// </summary>
    public void TargetAttack<T>(T target, int damage, bool isStun, DamageType type, bool isCritical) where T : Component
    {
        // 배틀 시스템은 배틀 시스템 끼리 통신 
        // 플레이어 <-> 배틀시스템 <-> 배틀시스템 <->좀비
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDamage(damage, isStun, type, isCritical); // 상대를 공격
    }
    /// <summary>
    /// 가진 모든 디버프 주면서 공격
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDamageWithDebuff(damage, isStun, _hitAdditionalList); // 상대를 공격
    }
    /// <summary>
    /// 가진 모든 디버프 주면서 공격 (타입, 치명타)
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDamageWithDebuff(damage, isStun, _hitAdditionalList, isCritical); // 상대를 공격
    }
    /// <summary>
    /// 가진 모든 디버프 주면서 공격 (타입)
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, DamageType type) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDamageWithDebuff(damage, isStun, _hitAdditionalList, type); // 상대를 공격
    }
    /// <summary>
    /// 가진 모든 디버프 주면서 공격 (타입, 치명타)
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, DamageType type, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDamageWithDebuff(damage, isStun, _hitAdditionalList, type, isCritical); // 상대를 공격
    }
    /// <summary>
    /// 특정 디버프만 주면서 공격
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, HitAdditional debuff) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDamageWithDebuff(damage, isStun, debuff); // 상대를 공격
    }
    /// <summary>
    /// 특정 디버프만 주면서 공격 (타입, 치명타)
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, HitAdditional debuff, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDamageWithDebuff(damage, isStun, debuff, isCritical); // 상대를 공격
    }
    /// <summary>
    /// 특정 디버프만 주면서 공격 (타입)
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, HitAdditional debuff, DamageType type) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDamageWithDebuff(damage, isStun, debuff, type); // 상대를 공격
    }
    /// <summary>
    /// 특정 디버프만 주면서 공격 (타입, 치명타)
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, HitAdditional debuff, DamageType type, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDamageWithDebuff(damage, isStun, debuff, type, isCritical); // 상대를 공격
    }
    /// <summary>
    /// 특정 디버프들을 주면서 공격 가능
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, List<HitAdditional> debuffs) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDamageWithDebuff(damage, isStun, debuffs); // 상대를 공격
    }
    /// <summary>
    /// 특정 디버프들을 주면서 공격 가능 (타입, 치명타)
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, List<HitAdditional> debuffs, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDamageWithDebuff(damage, isStun, debuffs, isCritical); // 상대를 공격
    }
    /// <summary>
    /// 특정 디버프들을 주면서 공격 가능 (타입)
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, List<HitAdditional> debuffs, DamageType type) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDamageWithDebuff(damage, isStun, debuffs, type); // 상대를 공격
    }
    /// <summary>
    /// 특정 디버프들을 주면서 공격 가능 (타입, 치명타)
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, List<HitAdditional> debuffs, DamageType type, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeDamageWithDebuff(damage, isStun, debuffs, type, isCritical); // 상대를 공격
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
    public void TakeDamage(int damage, bool isStun)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage);
    }
    /// <summary>
    /// 디버프 안주는 공격 맞기 (타입, 치명타 체크)
    /// </summary>
    public void TakeDamage(int damage, bool isStun, bool isCritical)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, isCritical);
    }
    /// <summary>
    /// 디버프 안주는 공격 맞기 (타입)
    /// </summary>
    public void TakeDamage(int damage, bool isStun, DamageType type)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage,type);
    }
    /// <summary>
    /// 디버프 안주는 공격 맞기 (타입, 치명타 체크)
    /// </summary>
    public void TakeDamage(int damage, bool isStun, DamageType type, bool isCritical)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, type, isCritical);
    }
    /// <summary>
    /// 공격받으면서 디버프 전부 받기
    /// </summary>
    public void TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> debuffs)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage);
        // 디버프 추가
        foreach (HitAdditional hitAdditional in debuffs) 
        {
            AddDebuff(hitAdditional);
        }
    }
    /// <summary>
    /// 공격받으면서 디버프 전부 받기 (타입)
    /// </summary>
    public void TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> debuffs, bool isCritical)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, isCritical);
        // 디버프 추가
        foreach (HitAdditional hitAdditional in debuffs)
        {
            AddDebuff(hitAdditional);
        }
    }
    /// <summary>
    /// 공격받으면서 디버프 전부 받기 (타입)
    /// </summary>
    public void TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> debuffs, DamageType type)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, type);
        // 디버프 추가
        foreach (HitAdditional hitAdditional in debuffs)
        {
            AddDebuff(hitAdditional);
        }
    }
    /// <summary>
    /// 공격받으면서 디버프 전부 받기 (타입, 치명타)
    /// </summary>
    public void TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> debuffs, DamageType type, bool isCritical)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, type, isCritical);
        // 디버프 추가
        foreach (HitAdditional hitAdditional in debuffs)
        {
            AddDebuff(hitAdditional);
        }
    }
    /// <summary>
    /// 특정 디버프만 받기
    /// </summary>
    public void TakeDamageWithDebuff(int damage, bool isStun, HitAdditional debuff)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage);

        AddDebuff(debuff);
    }
    /// <summary>
    /// 특정 디버프만 받기 (치명타)
    /// </summary>
    public void TakeDamageWithDebuff(int damage, bool isStun, HitAdditional debuff, bool isCritical)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, isCritical);

        AddDebuff(debuff);
    }
    /// <summary>
    /// 특정 디버프만 받기 (타입)
    /// </summary>
    public void TakeDamageWithDebuff(int damage, bool isStun, HitAdditional debuff, DamageType type)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage,type);

        AddDebuff(debuff);
    }
    /// <summary>
    /// 특정 디버프만 받기 (타입, 치명타)
    /// </summary>
    public void TakeDamageWithDebuff(int damage, bool isStun, HitAdditional debuff, DamageType type, bool isCritical)
    {
        // 데미지 주기
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, type, isCritical);

        AddDebuff(debuff);
    }
    #endregion
    #region 데미지 UI 
    /// <summary>
    /// 데미지 UI 띄우기
    /// </summary>
    private void CreateDamageText(int damage)
    {
        if (gameObject.tag == Tag.Player)
            return;

        DamageText text = Instantiate(DataContainer.GetDamageText(DamageType.Default), transform.position, Quaternion.identity);
        text.SetDamageText(damage, _hitTextPoint);
    }
    /// <summary>
    /// 데미지 UI 띄우기
    /// </summary>
    private void CreateDamageText(int damage, bool isCritical)
    {
        if (gameObject.tag == Tag.Player)
            return;

        DamageText text = Instantiate(DataContainer.GetDamageText(DamageType.Default), transform.position, Quaternion.identity);
        text.SetDamageText(damage, _hitTextPoint, DamageType.Default, isCritical);
    }
    /// <summary>
    /// 데미지 UI 띄우기
    /// </summary>
    private void CreateDamageText(int damage, DamageType type)
    {
        if (gameObject.tag == Tag.Player)
            return;

        DamageText text = Instantiate(DataContainer.GetDamageText(DamageType.Default), transform.position, Quaternion.identity);
        text.SetDamageText(damage, _hitTextPoint, type, false);
    }
    /// <summary>
    /// 데미지 UI 띄우기
    /// </summary>
    private void CreateDamageText(int damage, DamageType type, bool isCritical)
    {
        if (gameObject.tag == Tag.Player)
            return;

        DamageText text = Instantiate(DataContainer.GetDamageText(DamageType.Default), transform.position, Quaternion.identity);
        text.SetDamageText(damage, _hitTextPoint, type, isCritical);
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
        int index = _hitAdditionalList.FindIndex(origin => origin.Origin.Equals(hitAdditional.Origin));
        // 중복시 등록안함
        if (index != -1)
            return;
        _hitAdditionalList.Add(hitAdditional);
    }
    /// <summary>
    /// 적중 효과 삭제
    /// </summary>
    /// <param name="hitAdditional"></param>
    public void RemoveHitAdditionalList(HitAdditional hitAdditional)
    {
        // 중복체크
        int index = _hitAdditionalList.FindIndex(origin => origin.Origin.Equals(hitAdditional.Origin));
        // 중복 없을 시 삭제 안함
        if (index == -1)
            return;
        _hitAdditionalList.Remove(hitAdditional);
    }
    #endregion
    #region 디버프 추가/삭제
    /// <summary>
    /// 디버프 추가
    /// </summary>
    /// <param name="debuff"></param>
    private void AddDebuff(HitAdditional debuff)
    {
        int index = _debuffList.FindIndex(origin => origin.Origin.Equals(debuff.Origin));
        if (index >= _debuffList.Count)
            return;

        HitAdditional cloneDebuff = Instantiate(debuff);
        // 디버프 중복 시
        if (index != -1)
        {
            // 기존 디버프 삭제
            _debuffList[index].Exit();
            Destroy(_debuffList[index]);
            _debuffList.RemoveAt(index);
        }
        // 디버프 추가 후 발동
        _debuffList.Add(cloneDebuff);
        cloneDebuff.Origin = debuff.Origin;
        cloneDebuff.Battle = this;
        cloneDebuff.transform = transform;
        cloneDebuff.Enter(); // 디버프 발동
    }
    /// <summary>
    /// 디버프 삭제
    /// </summary>
    /// <param name="debuff"></param>
    private void RemoveDebuff(HitAdditional debuff)
    {
        debuff.Exit();
        _debuffList.Remove(debuff);
    }
    #endregion
    /// <summary>
    /// 디버프 종료 호출
    /// </summary>
    public void EndDebuff(HitAdditional debuff)
    {
        RemoveDebuff(debuff);
    }
    #region 콜백
    public void Enter()
    {
        foreach (HitAdditional debuff in _debuffList) 
        {
            debuff.Enter();
        }
    }

    public void Exit()
    {
        foreach (HitAdditional debuff in _debuffList)
        {
            debuff.Exit();
        }
    }

    public void Update()
    {
        foreach (HitAdditional debuff in _debuffList)
        {
            debuff.Update();
        }
    }

    public void FixedUpdate()
    {
        foreach (HitAdditional debuff in _debuffList)
        {
            debuff.FixedUpdate();
        }
    }

    public void Trigger()
    {
        foreach (HitAdditional debuff in _debuffList)
        {
            debuff.Trigger();
        }
    }

    public void TriggerFirst()
    {
        foreach (HitAdditional debuff in _debuffList)
        {
            debuff.TriggerFirst();
        }
    }
    #endregion
}
