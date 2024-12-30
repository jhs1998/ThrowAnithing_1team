using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveState : PlayerState
{
    public InteractiveState(PlayerController controller) : base(controller)
    {
        CantChangeState= true;
    }
    public override void Enter()
    {
        Rb.velocity = new Vector3(0,Rb.velocity.y,0);
    }

    public override void Update()
    {
        if (Input.GetButtonDown(InputKey.Negative))
        {
            ChangeState(PlayerController.State.Idle);
        }
    }
}
