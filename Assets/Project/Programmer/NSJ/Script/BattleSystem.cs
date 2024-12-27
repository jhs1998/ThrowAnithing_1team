using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BattleSystem : MonoBehaviour, IBattle
{
    public IHit Hitable { get; set; }
    public IDebuff Debuffable { get; set; }

    [SerializeField] private List<HitAdditional> _hitAdditionalList;
    [SerializeField] private List<HitAdditional> _debuffList;

    private void Awake()
    {
        Hitable = GetComponent<IHit>();
        Debuffable = GetComponent<IDebuff>();
    }
    public void TakeAttack<T>(T target, int damage, bool isStun) where T : Component
    {
        // 배틀 시스템은 배틀 시스템 끼리 통신 
        // 플레이어 <-> 배틀시스템 <-> 배틀시스템 <->좀비
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // 상대 배틀시스템 추적
        battle.TakeAttack(damage, isStun, _hitAdditionalList); // 상대 공격
    }

    public void TakeAttack(int damage, bool isStun, List<HitAdditional> hitAdditionals)
    {
        // 데미지 주기
        Hitable.TakeDamage(damage, isStun);
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
        int index = _debuffList.FindIndex(origin => origin.Origin.Equals(hitAdditional.Origin));
        // 중복시 등록안함
        if (index != -1)
            return;
        _hitAdditionalList.Add(hitAdditional);
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
        cloneDebuff.Target = gameObject;
        cloneDebuff.Battle = this;
        cloneDebuff.Enter();
    }
    /// <summary>
    /// 디버프 삭제
    /// </summary>
    /// <param name="debuff"></param>
    private void RemoveDebuff(HitAdditional debuff)
    {
        _debuffList.Remove(debuff);
    }


}
