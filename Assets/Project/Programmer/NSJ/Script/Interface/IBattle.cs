using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IBattle
{
    IHit Hit { get; set; }
    IDebuff Debuff { get; set; }

    public event UnityAction<int, bool> OnTargetAttackEvent;
    public event UnityAction<int, bool> OnTakeDamageEvent;
    public event UnityAction OnDieEvent;
    public void TakeDebuff(HitAdditional debuff);
    public void TakeDebuff(List<HitAdditional> debuff);
    public int TakeDamage(int damage, bool IsIgnoreDef);
    public int TakeDamage(int damage, bool isCritical, bool IsIgnoreDef);
    public int TakeDamage(int damage, CrowdControlType type, bool IsIgnoreDef);
    public int TakeDamage(int damage, CrowdControlType type, bool isCritical, bool IsIgnoreDef);
    public int TakeDamageWithDebuff(int damage, HitAdditional hitAdditional, bool IsIgnoreDef);
    public int TakeDamageWithDebuff(int damage, HitAdditional hitAdditional, bool isCritical, bool IsIgnoreDef);
    public int TakeDamageWithDebuff(int damage, CrowdControlType type, HitAdditional hitAdditional, bool IsIgnoreDef);
    public int TakeDamageWithDebuff(int damage, CrowdControlType type, HitAdditional hitAdditional, bool isCritical, bool IsIgnoreDef);
    public int TakeDamageWithDebuff(int damage, List<HitAdditional> hitAdditionals, bool IsIgnoreDef);
    public int TakeDamageWithDebuff(int damage,  List<HitAdditional> hitAdditionals, bool isCritical, bool IsIgnoreDef);
    public int TakeDamageWithDebuff(int damage, CrowdControlType type, List<HitAdditional> hitAdditionals, bool IsIgnoreDef);
    public int TakeDamageWithDebuff(int damage, CrowdControlType type, List<HitAdditional> hitAdditionals,  bool isCritical, bool IsIgnoreDef);
    public void Enter();
    public void Exit();
    public void Update();
    public void FixedUpdate();
    public void Trigger();
    public void EndDebuff(HitAdditional debuff);
}
