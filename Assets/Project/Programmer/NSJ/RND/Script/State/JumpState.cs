using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(PlayerController controller) : base(controller)
    {
        View.OnJumpEvent += Jump;
    }

    public override void Enter()
    {
        View.SetTrigger(PlayerView.Parameter.Jump);
    }

    public override void FixedUpdate()
    {
        
    }

    private void Jump()
    {
        Rb.AddForce(Vector3.up * 5f , ForceMode.Impulse);
    }
    
    private void CheckGround()
    {
        if(Physics.Raycast(transform.position, Vector3.down, 0.2f))
        {
            // TODO: 지면 체크 로직
        }
    }
}
