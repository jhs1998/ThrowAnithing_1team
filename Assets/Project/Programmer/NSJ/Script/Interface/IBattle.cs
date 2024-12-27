using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattle
{
    IHit Hit { get; set; }
    IDebuff Debuff { get; set; }

    void TakeAttack(int damage, bool isStun);
    void TakeAttackWithDebuff(int damage, bool isStun, List<HitAdditional> hitAdditionals);
    void TakeAttackWithDebuff(int damage, bool isStun, HitAdditional hitAdditional);
    void Enter();
    void Exit();
    void Update();
    void FixedUpdate();
    void Trigger();
    void TriggerFirst();
    void EndDebuff(HitAdditional debuff);
}
