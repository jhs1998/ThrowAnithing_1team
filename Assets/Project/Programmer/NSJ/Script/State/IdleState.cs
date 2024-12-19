using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerController controller) : base(controller)
    {

    }

    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;
        View.SetBool(PlayerView.Parameter.Idle, true);
    }

    public override void Update()
    {
        //Debug.Log("Idle");

        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        // 이동키 입력시 Run
        if (moveDir != Vector3.zero)
        {
            ChangeState(PlayerController.State.Run);
        }
        // 1번 공격키 입력시 근접공격
        else if (Input.GetButtonDown("Fire1"))
        {
            ChangeState(PlayerController.State.MeleeAttack);
        }
        // 2번 공격키 입력시 투척 공격
        else if (Input.GetButtonDown("Fire2"))
        {
            ChangeState(PlayerController.State.ThrowAttack);
        }
        // 지면에서 점프 키 입력 시 점프
        else if (Player.IsGround == true && Input.GetButtonDown("Jump"))
        {
            ChangeState(PlayerController.State.Jump);
        }
        // 공중에서 y축 물리값 음수일때 추락
        else if (Player.IsGround == false && Rb.velocity.y <= -1f)
        {
            ChangeState(PlayerController.State.Fall);
        }
    }
    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {
        View.SetBool(PlayerView.Parameter.Idle, false);
    }
}
