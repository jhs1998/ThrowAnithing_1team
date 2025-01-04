using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattle
{
    IHit Hit { get; set; }
    IDebuff Debuff { get; set; }
    public Transform HitTransform { get; set; }
    public void ITakeDebuff(HitAdditional debuff);
    public void ITakeDebuff(List<HitAdditional> debuff);
    public void ITakeAttack(int damage, bool isStun);
    public void ITakeAttackWithDebuff(int damage, bool isStun, HitAdditional hitAdditional);
    public void ITakeAttackWithDebuff(int damage, bool isStun, List<HitAdditional> hitAdditionals);
    public void Enter();
    public void Exit();
    public void Update();
    public void FixedUpdate();
    public void Trigger();
    public void TriggerFirst();
    public void EndDebuff(HitAdditional debuff);
}
