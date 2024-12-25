using UnityEngine;

public class DoubleJumpState : PlayerState
{
    public DoubleJumpState(PlayerController controller) : base(controller)
    {
        UseStamina = true;
    }

    public override void Enter()
    {
        View.SetTrigger(PlayerView.Parameter.DoubleJump);
    }
    public override void Exit()
    {

    }

    public override void Update()
    {
        // Debug.Log("Jump");
    }

    public override void OnTrigger()
    {
        Jump();
    }

    private void Jump()
    {
        
        // 임시 물리량 저장
        Rb.velocity = new Vector3(Rb.velocity.x, 0, Rb.velocity.z); // y값 물리량 제거
        if (MoveDir != Vector3.zero)
        {
            Player.LookAtMoveDir();
            Player.ChangeVelocityPlayerFoward();
        }

        Rb.AddForce(Vector3.up * Model.JumpPower, ForceMode.Impulse);
        // 점프 이후 바로 추락 모드 실행
        ChangeState(PlayerController.State.Fall);
    }

    private void Temp()
    {

    }
}
