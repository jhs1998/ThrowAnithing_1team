using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerController controller) : base(controller)
    {

    }

    public override void Enter()
    {
        _player.View.SetBool(PlayerView.Parameter.Idle, true);
    }

    public override void Update()
    {
        //Debug.Log("Idle");

        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (moveDir != Vector3.zero) 
        {
            _player.ChangeState(PlayerController.State.Run);
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            _player.ChangeState(PlayerController.State.MeleeAttack);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            _player.ChangeState(PlayerController.State.ThrowAttack);
        }
    }

    public override void Exit()
    {
        _player.View.SetBool(PlayerView.Parameter.Idle, false);
    }
}
