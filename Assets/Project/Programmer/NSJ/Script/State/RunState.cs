using UnityEngine;

public class RunState : PlayerState
{
    public RunState(PlayerController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        View.SetTrigger(PlayerView.Parameter.Run);
    }

    public override void Update()
    {
        CheckChangeState();
    }

    public override void FixedUpdate()
    {
        Run();
    }

    public override void Exit()
    {
        
    }

    private void Run()
    { 
        Player.LookAtMoveDir();

        // 플레이어 이동
        // 지상에 있고 벽에 부딪히지 않은 상태에서만 이동
        if (Player.IsGround == false && Player.IsWall == true)
            return;

        Vector3 originRb = Rb.velocity;
        Vector3 velocityDir = transform.forward * Model.MoveSpeed;
        Rb.velocity = new Vector3(velocityDir.x, originRb.y, velocityDir.z);
    }

    private void CheckChangeState()
    {
        // 이동키 입력이 없을때 평상시 모드
        if (MoveDir == Vector3.zero)
        {
            ChangeState(PlayerController.State.Idle);
        }
        // 1번 공격키 입력시 근접 공격
        else if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeState(PlayerController.State.MeleeAttack);
        }
        // 2번 공격키 입력 시 투척 공격
        else if (Input.GetButtonDown("Fire1"))
        {
            ChangeState(PlayerController.State.ThrowAttack);
        }
        // 특수공격 키 입력시 특수 공격
        else if (Input.GetButtonDown("Fire2"))
        {
            ChangeState(PlayerController.State.SpecialAttack);
        }
        // 지면에서 점프 키 입력 시 점프
        else if (Player.IsGround == true && Input.GetButtonDown("Jump"))
        {
            ChangeState(PlayerController.State.Jump);
        }
        // 공중에서 떨어질 시 추락
        else if (Player.IsGround == false && Rb.velocity.y <= -1f)
        {
            ChangeState(PlayerController.State.Fall);
        }
        // 드레인 키를 눌렀을 경우
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            ChangeState(PlayerController.State.Drain);
        }
    }

}
