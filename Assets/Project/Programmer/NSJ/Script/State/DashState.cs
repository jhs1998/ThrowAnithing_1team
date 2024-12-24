using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    private Vector3 _moveDir;

    private bool _isInputJumpDown;
    Coroutine _checkInputRoutine;
    public DashState(PlayerController controller) : base(controller)
    {
        UseStamina = true;
    }

    public override void Enter()
    {
        InputKey();
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
        if (_isInputJumpDown)
        {
            ChangeState(PlayerController.State.Fall);
        }
        else
        {
            ChangeState(PlayerController.State.Idle);
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
    /// 대쉬
    /// </summary>
    public void Dash()
    {

        Player.LookAtMoveDir(_moveDir);

        Rb.velocity = transform.forward * Model.DashPower;
    }
    /// <summary>
    /// 방향키 입력 확인
    /// </summary>ㄴ
    private void InputKey()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        _moveDir = new Vector3(x, 0, z);
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
