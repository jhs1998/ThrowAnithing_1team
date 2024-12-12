using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerState
{
    Vector3 _moveDir;
    public RunState(PlayerController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        Debug.Log("Run");
    }

    public override void Update()
    {
        InputKey();
    }

    public override void FixedUpdate()
    {
        Run();
    }

    public override void Exit()
    {
        PlayAnimation();
        _player.Rb.velocity = Vector3.zero;
    }

    private void InputKey()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        _moveDir = new Vector3(x, 0, z);

        if(_moveDir == Vector3.zero)
        {
            _player.ChangeState(PlayerController.State.Idle);
        }
    }

    private void Run()
    {
        PlayAnimation();
        // 카메라 방향으로 플레이어가 바라보게
        _player.transform.rotation = _player.CamareArm.rotation;
        // 카메라는 다시 로컬 기준 0,0,0 의 방향
        _player.CamareArm.localRotation = Quaternion.identity;
        _player.CamareArm.SetParent(null);
        
        //Vector3 moveDir = 

        _player.transform.rotation = Quaternion.LookRotation(_moveDir);
        _player.Rb.velocity = _player.transform.forward * _player.Model.MoveSpeed;

        _player.CamareArm.SetParent(_player.transform);
    }

    private void PlayAnimation()
    {
        float animParam = Mathf.Max(Mathf.Abs(_moveDir.x), Mathf.Abs(_moveDir.z));
        _player.View.SetFloat(PlayerView.Parameter.MoveSpeed, animParam);
    }
}
