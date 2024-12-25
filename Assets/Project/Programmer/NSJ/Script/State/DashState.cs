using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    private bool _isInputJumpDown;
    Coroutine _checkInputRoutine;
    public DashState(PlayerController controller) : base(controller)
    {
        UseStamina = true;
    }

    public override void Enter()
    {
        Player.LookAtMoveDir();
        View.SetTrigger(PlayerView.Parameter.Dash);
    }
    public override void Exit()
    {
        if (_checkInputRoutine != null)
        {
            CoroutineHandler.StopRoutine(_checkInputRoutine);
            _checkInputRoutine = null;
        }
    }
    public override void Update()
    {
        Dash();
    }
    public override void EndAnimation()
    {
        if(Player.IsGround == true)
        {
            ChangeState(PlayerController.State.Idle);
        }
        else
        {
            Rb.velocity /= 2;
            ChangeState(PlayerController.State.Fall);
        }

    }

    public override void OnCombo()
    {
        if(_checkInputRoutine == null)
        {
            _checkInputRoutine = CoroutineHandler.StartRoutine(CheckInputRoutine());
        }
    }
    public override void EndCombo()
    {
        if (_checkInputRoutine != null) 
        {
            CoroutineHandler.StopRoutine(_checkInputRoutine);
            _checkInputRoutine = null;
        }
    }
    /// <summary>
    /// ´ë½¬
    /// </summary>
    public void Dash()
    {     
        Rb.velocity = transform.forward * Model.DashPower;
    }

    IEnumerator CheckInputRoutine()
    {
        while (true)
        {
            if(Player.PrevState == PlayerController.State.DoubleJump)
            {
                _isInputJumpDown = true;
            }
            yield return null;
        }
    }
}
