using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : PlayerState
{
    private float _atttackBufferTime;
    private bool _isCombe;
    
    public MeleeAttackState(PlayerController controller) : base(controller)
    {
        _atttackBufferTime = _player.AttackBufferTime;
    }

    public override void Enter()
    {
        if (_player.View.GetBool(PlayerView.Parameter.ComboAttack) == false)
        {
            _player.View.SetTrigger(PlayerView.Parameter.MeleeAttack);
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
        yield return null;
        float timeCount = _atttackBufferTime;
        while (_player.View.IsAnimationFinish == false)
        {
            // 공격 버퍼
            if (Input.GetButtonDown("Fire1"))
            {
                // 다음 공격 대기
                _isCombe = true;
                _player.View.SetBool(PlayerView.Parameter.ComboAttack, true);
                timeCount = _atttackBufferTime;
            }
            timeCount -= Time.deltaTime;
            if(timeCount <= 0)
            {
                // 다음 공격 취소
                _isCombe = false;
                _player.View.SetBool(PlayerView.Parameter.ComboAttack, false);
                timeCount = _atttackBufferTime;
            }

            yield return null;
        }

        if (_isCombe == true)
        {
            _player.ChangeState(PlayerController.State.MeleeAttack);
        }
        else
        {
            _player.ChangeState(PlayerController.State.Idle);
        }
        
    }
}
