using System.Collections;
using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(PlayerController controller) : base(controller)
    {
        UseStamina = true;
        StaminaAmount = Model.JumpStamina;
    }
    public override void InitArm()
    {
        StaminaAmount = Model.JumpStamina;
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
       // Debug.Log("PrevJump");
    }

    public override void FixedUpdate()
    {
      
    }
    public override void OnTrigger()
    {
        Jump();
    }

    private void Jump()
    {
        // �ӽ� ������ ����
        Vector3 tempVelocity = Rb.velocity;
        tempVelocity.y = 0;
        Rb.velocity = tempVelocity;

        Rb.AddForce(Vector3.up * Model.JumpPower, ForceMode.Impulse);
        // ���� ���� �ٷ� �߶� ��� ����
        ChangeState(PlayerController.State.Fall);
    }
}
