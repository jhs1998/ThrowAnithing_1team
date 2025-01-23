using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Power JumpAttack", menuName = "Arm/AttackType/Power/JumpAttack")]
public class PowerJumpAttack : ArmJumpAttack
{
    [Header("�÷��̾ ������� ��¦ Ƣ������� ����")]
    [SerializeField] private float _popValue;
    [Header("�ϴ� ���� ����")]
    [SerializeField] private float m_downAngle;
    private float _downAngle
    {
        get
        {
            if (Player.IsVerticalCameraMove == true)
                return 0;
            else
                return m_downAngle;
        }
    }
    public override void Enter()
    {
        // �ִϸ��̼� ����
        View.SetTrigger(PlayerView.Parameter.JumpAttack);

    }

    public override void OnTrigger()
    {
        // ĳ���� ��¦ Ƣ�������

        // �Է��� �������� �÷��̾� ���� ��ȯ
        Player.LookAtMoveDir();
        // �÷��̾� ������ ��ȯ
        Rb.velocity = new Vector3(Rb.velocity.x, 0, Rb.velocity.z);
        if (Player.MoveDir != Vector3.zero)
            Player.ChangeVelocityPlayerFoward();

        // �÷��̾� ī�޶� ���� �ٶ󺸱�
        Player.LookAtAttackDir();
        Rb.AddForce(Vector3.up * _popValue, ForceMode.Impulse);
        ThrowObject();
    }
    public override void Exit()
    {
        Player.LookAtCameraFoward();
    }
    public override void EndAnimation()
    {
        if(Player.IsDoubleJump == false)
        {
            ChangeState(PlayerController.State.Fall);
        }
        else
        {
            ChangeState(PlayerController.State.DoubleJumpFall);
        }
      
    }

    private void ThrowObject()
    {
        //int throwObjectID = Model.ThrowObjectStack.Count > 0 ? Model.PopThrowObject().ID : 0;
        float downAngle = Player.IsTargetHolding || Player.IsTargetToggle ? 0 : _downAngle;

        int throwObjectID = Model.ThrowObjectStack.Count > 0 ? Model.PopThrowObject().ID : 0;

        Quaternion _muzzleRot = Quaternion.Euler(_muzzlePoint.eulerAngles.x + downAngle, _muzzlePoint.eulerAngles.y, _muzzlePoint.eulerAngles.z);
        ThrowObject throwObject = Instantiate(DataContainer.GetThrowObject(throwObjectID), _muzzlePoint.position, _muzzleRot);
        throwObject.Init(Player , CrowdControlType.Stiff, false, (int)Model.PowerThrowAttack[0],  Model.ThrowAdditionals);
        throwObject.Shoot(Player.ThrowPower);
    }
}
