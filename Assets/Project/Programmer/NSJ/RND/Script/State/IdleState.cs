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

    }

    public override void Update()
    {
        Debug.Log("Idle");

        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (moveDir != Vector3.zero) 
        {
            _player.ChangeState(PlayerController.State.Run);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            _player.ChangeState(PlayerController.State.MeleeAttack);
        }
    }


    public override void Exit()
    {
        
    }
}
