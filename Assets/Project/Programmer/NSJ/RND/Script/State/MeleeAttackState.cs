using System.Collections;
using UnityEngine;

public class MeleeAttackState : PlayerState
{
    private float _atttackBufferTime;
    private bool _isCombe;
    private bool _isChangeAttack;
    public MeleeAttackState(PlayerController controller) : base(controller)
    {
        _atttackBufferTime = _player.AttackBufferTime;
    }

    public override void Enter()
    {
        _isChangeAttack = false;
        if (_player.View.GetBool(PlayerView.Parameter.MeleeCombo) == false)
        {
            _player.View.SetTrigger(PlayerView.Parameter.MeleeAttack);
        }
        else
        {
            _player.Model.MeleeComboCount++;
        }
        CoroutineHandler.StartRoutine(MeleeAttackRoutine());
    }

    public override void Update()
    {
        //Debug.Log("Melee");
    }

    public override void Exit()
    {

    }

    IEnumerator MeleeAttackRoutine()
    {
        if (_player.IsAttackFoward == true)
        {
            // 카메라 방향으로 플레이어가 바라보게
            Quaternion cameraRot = Quaternion.Euler(0, _player.CamareArm.eulerAngles.y, 0);
            _player.transform.rotation = cameraRot;
            // 카메라는 다시 로컬 기준 전방 방향
            if (_player.CamareArm.parent != null)
            {
                _player.CamareArm.localRotation = Quaternion.Euler(_player.CamareArm.localRotation.eulerAngles.x, 0, 0);
            }
        }

        yield return null;
        float timeCount = _atttackBufferTime;
        while (_player.View.IsAnimationFinish == false)
        {       
            // 공격 버퍼
            if (Input.GetButtonDown("Fire1"))
            {
                // 다음 공격 대기
                _isCombe = true;
                _player.View.SetBool(PlayerView.Parameter.MeleeCombo, true);
                timeCount = _atttackBufferTime;
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                // 던지기 공격 전환
                _isCombe = false;
                _isChangeAttack = true;
                _player.View.SetBool(PlayerView.Parameter.MeleeCombo, false);
                timeCount = _atttackBufferTime;
            }

            // 버퍼 타이머
            timeCount -= Time.deltaTime;
            if (timeCount <= 0)
            {
                // 다음 공격 취소
                _isCombe = false;
                _player.View.SetBool(PlayerView.Parameter.MeleeCombo, false);
                timeCount = _atttackBufferTime;
            }

            yield return null;
        }

        if (_isCombe == true)
        {
            _player.ChangeState(PlayerController.State.MeleeAttack);
        }
        else if (_isChangeAttack == true)
        {
            _player.Model.MeleeComboCount = 0;
            _player.ChangeState(PlayerController.State.ThrowAttack);
        }
        else
        {
            _player.Model.MeleeComboCount = 0;
            _player.ChangeState(PlayerController.State.Idle);
        }

    }
}
