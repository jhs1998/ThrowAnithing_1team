using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattle
{
    IHit Hitable { get; set; }
    IDebuff Debuffable { get; set; }

    void TakeAttack(int damage, bool isStun, List<HitAdditional> hitAdditionals);
    void Enter();
    void Exit();
    void Update();
    void FixedUpdate();
    void Trigger();
    void TriggerFirst();
}
