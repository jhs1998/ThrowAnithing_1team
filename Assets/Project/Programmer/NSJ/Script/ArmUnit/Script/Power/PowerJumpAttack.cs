using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Power JumpAttack", menuName = "Arm/AttackType/Power/JumpAttack")]
public class PowerJumpAttack : ArmJumpAttack
{
    [Header("플레이어가 허공에서 살짝 튀어오르는 정도")]
    [SerializeField] private float _popValue;
    [Header("하단 공격 각도")]
    [SerializeField] private float _downAngle;
    public override void Enter()
    {
        Debug.Log("점프어택 시작");
        // 애니메이션 실행
        View.SetTrigger(PlayerView.Parameter.JumpAttack);

    }

    public override void OnTrigger()
    {
        // 캐릭터 살짝 튀어오르기
        Player.LookAtCameraFoward();
        Rb.velocity = new Vector3(Rb.velocity.x, 0, Rb.velocity.z);
        Rb.AddForce(Vector3.up * _popValue, ForceMode.Impulse);
        ThrowObject();
    }
    public override void Exit()
    {
        Player.LookAtCameraFoward();
    }
    public override void EndAnimation()
    {
        ChangeState(PlayerController.State.Fall);
    }

    private void ThrowObject()
    {
        //int throwObjectID = Model.ThrowObjectStack.Count > 0 ? Model.PopThrowObject().ID : 0;
        Quaternion _muzzleRot = Quaternion.Euler(_muzzlePoint.eulerAngles.x + _downAngle, _muzzlePoint.eulerAngles.y, _muzzlePoint.eulerAngles.z);
        ThrowObject throwObject = Player.InstantiateObject(DataContainer.GetThrowObject(0), _muzzlePoint.position, _muzzleRot);
        throwObject.Init(Player, Model.HitAdditionals, Model.ThrowAdditionals);
        throwObject.Shoot(Player.ThrowPower);
        throwObject.TriggerFirstThrowAddtional();
    }
}
