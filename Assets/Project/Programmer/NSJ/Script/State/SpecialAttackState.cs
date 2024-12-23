using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackState : PlayerState
{
    public SpecialAttackState(PlayerController controller) : base(controller)
    {
    }
    public override void Enter()
    {
        Arm.Enter();
    }
    public override void Exit()
    {
        Arm.Exit();
    }

    public override void Update()
    {
        Arm.Update();
    }
    public override void FixedUpdate()
    {
        Arm.FixedUpdate();
    }

    public override void OnDrawGizmos()
    {
        Arm.OnDrawGizmos();
    }

    /// <summary>
    /// 오브젝트 던지기 공격
    /// </summary>
    public override void OnTrigger()
    {
        Arm.OnTrigger();

    }
    public override void EndAnimation()
    {
        Arm.EndAnimation();
    }

    public override void OnCombo()
    {
        Arm.OnCombo();
    }

    public override void EndCombo()
    {
        Arm.EndCombo();
    }
}
