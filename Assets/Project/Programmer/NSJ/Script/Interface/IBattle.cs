using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattle
{
    IHit Hit { get; set; }
    IDebuff Debuff { get; set; }
    public void TakeDebuff(HitAdditional debuff);
    public void TakeDebuff(List<HitAdditional> debuff);
    public int TakeDamage(int damage, bool isStun);
    public int TakeDamage(int damage, bool isStun, bool isCritical);
    public int TakeDamage(int damage, bool isStun, DamageType type);
    public int TakeDamage(int damage, bool isStun, DamageType type, bool isCritical);
    public int TakeDamageWithDebuff(int damage, bool isStun, HitAdditional hitAdditional);
    public int TakeDamageWithDebuff(int damage, bool isStun, HitAdditional hitAdditional, bool isCritical);
    public int TakeDamageWithDebuff(int damage, bool isStun, HitAdditional hitAdditional, DamageType type);
    public int TakeDamageWithDebuff(int damage, bool isStun, HitAdditional hitAdditional, DamageType type , bool isCritical);
    public int TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> hitAdditionals);
    public int TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> hitAdditionals, bool isCritical);
    public int TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> hitAdditionals, DamageType type);
    public int TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> hitAdditionals, DamageType type, bool isCritical);
    public void Enter();
    public void Exit();
    public void Update();
    public void FixedUpdate();
    public void Trigger();
    public void EndDebuff(HitAdditional debuff);
}
