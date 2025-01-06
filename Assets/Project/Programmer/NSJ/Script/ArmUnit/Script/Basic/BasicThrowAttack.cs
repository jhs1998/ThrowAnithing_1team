using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Basic Throw", menuName = "Arm/AttackType/Basic/Throw")]
public class BasicThrowAttack : ArmThrowAttack
{
    Coroutine _throwRoutine;
    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;

        // 첫 공격 시 첫 공격 애니메이션 실행
        if (Player.PrevState != PlayerController.State.ThrowAttack)
        {
            View.SetTrigger(PlayerView.Parameter.BasicThrow);
        }
        else
        {
            View.SetTrigger(PlayerView.Parameter.OnCombo);
        }

        if (Player.IsAttackFoward == true)
        {
            Player.LookAtAttackDir();
        }
    }
    public override void Exit()
    {
        if (_throwRoutine != null)
        {
            CoroutineHandler.StopRoutine(_throwRoutine);
            _throwRoutine = null;
        }
    }

    public override void OnTrigger()
    {
        ThrowObject();
    }
    public override void EndAnimation()
    {

    }
    public override void OnCombo()
    {
        if (_throwRoutine == null)
        {
            _throwRoutine = CoroutineHandler.StartRoutine(OnComboRoutine());
        }
    }
    public override void EndCombo()
    {
        if (_throwRoutine != null)
        {
            ChangeState(PlayerController.State.Idle);
        }
    }



    private void ThrowObject()
    {
        int throwObjectID = Model.ThrowObjectStack.Count > 0 ? Model.PopThrowObject().ID : 0;

        ThrowObject throwObject = Instantiate(DataContainer.GetThrowObject(throwObjectID), _muzzlePoint.position, _muzzlePoint.rotation);
        throwObject.Init(Player, (int)Model.PowerThrowAttack[0],Model.ThrowAdditionals);
        throwObject.Shoot(Player.ThrowPower);
    }
    IEnumerator OnComboRoutine()
    {
        while (true)
        {
            if (InputKey.GetButtonDown(InputKey.Throw))
            {
                ChangeState(PlayerController.State.ThrowAttack);
            }
            else if (InputKey.GetButtonDown(InputKey.Melee))
            {
                ChangeState(PlayerController.State.MeleeAttack);
            }
            yield return null;
        }
    }
}
