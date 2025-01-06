using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattle
{
    IHit Hit { get; set; }
    IDebuff Debuff { get; set; }
    public void TakeDebuff(HitAdditional debuff);
    public void TakeDebuff(List<HitAdditional> debuff);
    public void TakeDamage(int damage, bool isStun);
    public void TakeDamage(int damage, bool isStun, bool isCritical);
    public void TakeDamage(int damage, bool isStun, DamageType type);
    public void TakeDamage(int damage, bool isStun, DamageType type, bool isCritical);
    public void TakeDamageWithDebuff(int damage, bool isStun, HitAdditional hitAdditional);
    public void TakeDamageWithDebuff(int damage, bool isStun, HitAdditional hitAdditional, bool isCritical);
    public void TakeDamageWithDebuff(int damage, bool isStun, HitAdditional hitAdditional, DamageType type);
    public void TakeDamageWithDebuff(int damage, bool isStun, HitAdditional hitAdditional, DamageType type , bool isCritical);
    public void TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> hitAdditionals);
    public void TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> hitAdditionals, bool isCritical);
    public void TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> hitAdditionals, DamageType type);
    public void TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> hitAdditionals, DamageType type, bool isCritical);
    public void Enter();
    public void Exit();
    public void Update();
    public void FixedUpdate();
    public void Trigger();
    public void EndDebuff(HitAdditional debuff);
}
