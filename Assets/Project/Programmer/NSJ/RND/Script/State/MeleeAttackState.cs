using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : PlayerState
{
    public MeleeAttackState(PlayerController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        _player.View.SetTrigger(PlayerView.Parameter.MeleeAttack);
    }

    public override void Update()
    {
        if(_player.View.IsAnimationFinish == true)
        {
            _player.ChangeState(PlayerController.State.Idle);
        }
         
    }

    public override void Exit()
    {
        _player.View.IsAnimationFinish = false;
    }
}
