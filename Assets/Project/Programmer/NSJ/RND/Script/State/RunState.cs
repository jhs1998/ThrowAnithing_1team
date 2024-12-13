using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject.SpaceFighter;
using static UnityEngine.Rendering.DebugUI;

public class RunState : PlayerState
{
    Vector3 _moveDir;
    public RunState(PlayerController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        _player.View.SetBool(PlayerView.Parameter.Run, true);
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
        _player.View.SetBool(PlayerView.Parameter.Run, false);
        _player.Rb.velocity = Vector3.zero;
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
        _player.transform.rotation = _player.CamareArm.rotation;
        // 카메라는 다시 로컬 기준 0,0,0 의 방향
        _player.CamareArm.localRotation = Quaternion.identity;
        _player.CamareArm.SetParent(null);

        // 입력한 방향쪽을 플레이어가 바라봄
        Vector3 moveDir = _player.transform.forward * _moveDir.z + _player.transform.right * _moveDir.x;
        if (moveDir == Vector3.zero)
            return;
        _player.transform.rotation = Quaternion.LookRotation(moveDir);

        // 플레이어 이동
        _player.Rb.velocity = _player.transform.forward * _player.Model.MoveSpeed;

        _player.CamareArm.SetParent(_player.transform);
    }

    private void CheckChangeState()
    {
        if (_moveDir == Vector3.zero)
        {         
            _player.ChangeState(PlayerController.State.Idle);
        }
        
        else if (Input.GetButtonDown("Fire1"))
        {
            _player.ChangeState(PlayerController.State.MeleeAttack);
        }
    }

}
