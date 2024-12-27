using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattle
{
    IHit Hit { get; set; }
    IDebuff Debuff { get; set; }

    void TakeAttack(int damage, bool isStun, List<HitAdditional> hitAdditionals);
    void Enter();
    void Exit();
    void Update();
    void FixedUpdate();
    void Trigger();
    void TriggerFirst();
}
