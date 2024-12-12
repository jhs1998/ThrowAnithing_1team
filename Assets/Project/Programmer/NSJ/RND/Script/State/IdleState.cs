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
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Debug.Log("Idle");
        if (moveDir != Vector3.zero) 
        {
            _player.ChangeState(PlayerController.State.Run);
        }
    }


    public override void Exit()
    {
        Debug.Log("Idle ³¡");
    }
}
