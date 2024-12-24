using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpState : PlayerState
{
    Vector3 _moveDir;
    public DoubleJumpState(PlayerController controller) : base(controller)
    {
        UseStamina = true;
    }

    public override void Enter()
    {
        CheckMoveInput();

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

    private void CheckMoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        _moveDir = new Vector3(x, 0, z);
    }

    private void Jump()
    {
        Player.LookAtMoveDir(_moveDir);

        // 임시 물리량 저장
        Rb.velocity = new Vector3(Rb.velocity.x, 0, Rb.velocity.z); // y값 물리량 제거
        Vector3 tempVelocity = transform.forward *Rb.velocity.magnitude; // x,z 값의 물리량만 계산
        Rb.velocity = tempVelocity; // 대입

        Rb.AddForce(Vector3.up * Model.JumpPower, ForceMode.Impulse);
        // 점프 이후 바로 추락 모드 실행
        ChangeState(PlayerController.State.Fall);
    }

    private void Temp()
    {

    }
}
