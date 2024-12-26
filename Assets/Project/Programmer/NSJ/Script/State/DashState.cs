using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    Coroutine _checkInputRoutine;
    public DashState(PlayerController controller) : base(controller)
    {
        UseStamina = true;
    }

    public override void Enter()
    {
        Player.IsInvincible = true;
        Player.LookAtMoveDir();
        View.SetTrigger(PlayerView.Parameter.Dash);
    }
    public override void Exit()
    {
        Player.IsInvincible = false; 
    }
    public override void Update()
    {
        Dash();
    }
    public override void EndAnimation()
    {
        if(Player.IsGround != true)
        { 
            Rb.velocity /= 2;
            ChangeState(PlayerController.State.Fall);
        }
        else
        {
            ChangeState(PlayerController.State.Idle);
        }

    }
    /// <summary>
    /// ´ë½¬
    /// </summary>
    public void Dash()
    {     
        Rb.velocity = transform.forward * Model.DashPower;
    }
}
