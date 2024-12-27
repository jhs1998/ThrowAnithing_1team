using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour, IBattle
{
    public IHit Hit { get; set; }
    public IDebuff Debuff { get; set; }

    [SerializeField] private List<HitAdditional> _hitAdditionalList;
    [SerializeField] private List<HitAdditional> _debuffList;

    private void Awake()
    {
        Hit = GetComponent<IHit>();
        Debuff = GetComponent<IDebuff>();
    }
    public void TargetAttack<T>(T target, int damage, bool isStun) where T : Component
    {
        // 배틀 시스템은 배틀 시스템 끼리 통신 
        // 플레이어 <-> 배틀시스템 <-> 배틀시스템 <->좀비
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeAttack(damage, isStun, _hitAdditionalList); // 상대를 공격
    }

    public void TakeAttack(int damage, bool isStun, List<HitAdditional> hitAdditionals)
    {
        // 데미지 주기
        Hit.TakeDamage(damage, isStun);
        // 디버프 추가
        foreach (HitAdditional hitAdditional in hitAdditionals) 
        {
            AddDebuff(hitAdditional);
        }
    }
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
    /// <summary>
    /// 디버프 종료 호출
    /// </summary>
    public void EndDebuff(HitAdditional debuff)
    {
        RemoveDebuff(debuff);
    }

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
