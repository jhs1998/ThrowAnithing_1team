using UnityEngine;

public class RunState : PlayerState
{
    Vector3 _moveDir;
    public RunState(PlayerController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        Player.View.SetBool(PlayerView.Parameter.Run, true);
    }

    public override void Update()
    {
        //Debug.Log("Run");
        InputKey();
        CheckChangeState();
    }

    public override void FixedUpdate()
    {
        Run();
    }

    public override void Exit()
    {
        View.SetBool(PlayerView.Parameter.Run, false);   
    }

    private void InputKey()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        _moveDir = new Vector3(x, 0, z);
    }

    private void Run()
    {
        // 카메라 방향으로 플레이어가 바라보게
        Quaternion cameraRot = Quaternion.Euler(0, Player.CamareArm.eulerAngles.y, 0);
        transform.rotation = cameraRot;
        // 카메라는 다시 로컬 기준 전방 방향
        if (Player.CamareArm.parent != null)
        {
            // 카메라 흔들림 버그 잡아주는 코드
            Player.CamareArm.localPosition = new Vector3(0, Player.CamareArm.localPosition.y, 0);
            Player.CamareArm.localRotation = Quaternion.Euler(Player.CamareArm.localRotation.eulerAngles.x, 0, 0);
        }

        Player.CamareArm.SetParent(null);

        // 입력한 방향쪽을 플레이어가 바라봄
        Vector3 moveDir = transform.forward * _moveDir.z + transform.right * _moveDir.x;
        if (moveDir == Vector3.zero)
            return;
        transform.rotation = Quaternion.LookRotation(moveDir);

        // 플레이어 이동
        Vector3 originRb = Rb.velocity;
        Rb.velocity = transform.forward * Model.MoveSpeed;
        Rb.velocity = new Vector3(Rb.velocity.x, originRb.y, Rb.velocity.z);

        Player.CamareArm.SetParent(Player.transform);
    }

    private void CheckChangeState()
    {
        // 이동키 입력이 없을때 평상시 모드
        if (_moveDir == Vector3.zero)
        {
            ChangeState(PlayerController.State.Idle);
        }
        // 1번 공격키 입력시 근접 공격
        else if (Input.GetButtonDown("Fire1"))
        {
            ChangeState(PlayerController.State.MeleeAttack);
        }
        // 2번 공격키 입력 시 투척 공격
        else if (Input.GetButtonDown("Fire2"))
        {
            ChangeState(PlayerController.State.ThrowAttack);
        }
        // 지면에서 점프 키 입력 시 점프
        else if (Player.IsGround == true && Input.GetButtonDown("Jump"))
        {
            ChangeState(PlayerController.State.Jump);
        }
        // 공중에서 떨어질 시 추락
        else if (Player.IsGround == false && Rb.velocity.y <= -2f)
        {
            ChangeState(PlayerController.State.Fall);
        }
    }

}
