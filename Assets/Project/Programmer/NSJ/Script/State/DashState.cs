using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    private Vector3 _moveDir;
    public DashState(PlayerController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        InputKey();
        View.SetTrigger(PlayerView.Parameter.Dash);
    }

    public override void Update()
    {
        Dash();
        if(View.GetIsAnimFinish(PlayerView.Parameter.Dash) == true)
        {
            ChangeState(PlayerController.State.Idle);
        }
    }

    /// <summary>
    /// 대쉬
    /// </summary>
    public void Dash()
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
        {
            moveDir = transform.forward;
        }
        transform.rotation = Quaternion.LookRotation(moveDir);
            
        Rb.velocity = transform.forward * Model.MoveSpeed * 3;
        Player.CamareArm.SetParent(transform);
    }

    private void InputKey()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        _moveDir = new Vector3(x, 0, z);
    }
}
