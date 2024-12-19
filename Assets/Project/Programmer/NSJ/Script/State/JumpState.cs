using System.Collections;
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
    public override void Exit()
    {

    }

    public override void Update()
    {
       // Debug.Log("Jump");
    }

    public override void FixedUpdate()
    {
      
    }

    private void Jump()
    {
        // 임시 물리량 저장
        Vector3 tempVelocity = Rb.velocity;
        tempVelocity.y = 0;
        Rb.velocity = tempVelocity;

        Rb.AddForce(Vector3.up * Model.JumpPower, ForceMode.Impulse);
        // 점프 이후 바로 추락 모드 실행
        ChangeState(PlayerController.State.Fall);
    }
}
