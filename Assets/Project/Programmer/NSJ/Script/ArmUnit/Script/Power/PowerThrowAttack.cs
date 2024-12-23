using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="_power Throw", menuName = "Arm/AttackType/_power/Throw")]
public class PowerThrowAttack : ArmThrowAttack
{
    private float _chargeTime;
    private bool _isThrow;
    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;

        View.SetTrigger(PlayerView.Parameter.PowerThrow);
    }
    public override void Exit()
    {
        _chargeTime = 0;
        _isThrow = false;
    }
    public override void Update()
    {
        if (_isThrow == false)
        {
            _chargeTime += Time.deltaTime;
        }

        if (Input.GetButtonUp("Fire2") && _isThrow ==false)
        {
            _isThrow = true;
            // 카메라 방향으로 플레이어가 바라보게
            Quaternion cameraRot = Quaternion.Euler(0, Player.CamareArm.eulerAngles.y, 0);
            transform.rotation = cameraRot;
            // 카메라는 다시 로컬 기준 전방 방향
            if (Player.CamareArm.parent != null)
            {
                Player.CamareArm.localRotation = Quaternion.Euler(Player.CamareArm.localRotation.eulerAngles.x, 0, 0);
            }
            View.SetFloat(PlayerView.Parameter.Charge, _chargeTime);
            View.SetTrigger(PlayerView.Parameter.ChargeEnd);
        }
    }

    public override void OnTrigger()
    {
        ThrowObject();
    }
    public override void EndCombo()
    {
        ChangeState(PlayerController.State.Idle);
    }

    private void ThrowObject()
    {
        int throwObjectID = Model.ThrowObjectStack.Count > 0 ? Model.PopThrowObject().ID : 0;

        ThrowObject throwObject = Player.InstantiateObject(DataContainer.GetThrowObject(throwObjectID), _muzzlePoint.position, _muzzlePoint.rotation);
        throwObject.Init(Player, Model.HitAdditionals, Model.ThrowAdditionals);
        //TODO : 데미지 계산식 검토 필요
        if (_chargeTime > 1.25)
        {
            throwObject.Damage += 110;
        }
        else if (_chargeTime > 0.5)
        {
            throwObject.Damage += 70;
        }
        else
        {
            throwObject.Damage += 40;
        }
        throwObject.Shoot(Player.ThrowPower);
        throwObject.TriggerFirstThrowAddtional();
    }
}
