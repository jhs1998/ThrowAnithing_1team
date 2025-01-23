using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerController controller) : base(controller)
    {

    }

    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;

        View.SetBool(PlayerView.Parameter.Idle,true);
    }

    public override void Exit()
    {
        View.SetBool(PlayerView.Parameter.Idle, false);
    }
    public override void Update()
    {
        // �̵�Ű �Է½� Run, �̵��ӵ��� 0���ٴ� Ŭ��
        if (MoveDir != Vector3.zero && Model.MoveSpeed > 0)
        {
            ChangeState(PlayerController.State.Run);
        }
        // 1�� ����Ű �Է½� ��������
        else if (InputKey.GetButtonDown(InputKey.Melee))
        {
            ChangeState(PlayerController.State.MeleeAttack);
        }
        // 2�� ����Ű �Է½� ��ô ����
        else if (InputKey.GetButtonDown(InputKey.Throw))
        {
            ChangeState(PlayerController.State.ThrowAttack);
        }
        // Ư������ Ű �Է½� Ư�� ����
        else if (InputKey.GetButtonDown(InputKey.Special))
        {
            ChangeState(PlayerController.State.SpecialAttack);
        }
        // ���鿡�� ���� Ű �Է� �� ����
        else if (Player.IsGround == true && InputKey.GetButtonDown(InputKey.Jump))
        {
            ChangeState(PlayerController.State.Jump);
        }
        // ���߿��� y�� ������ �����϶� �߶�
        else if (Player.IsGround == false && Rb.velocity.y <= -1f && Player.IsNearGround == false)
        {
            ChangeState(PlayerController.State.Fall);
        }
        // �巹�� Ű�� ������ ���
        else if (InputKey.GetButtonDown(InputKey.Drain))
        {
            ChangeState(PlayerController.State.Drain);
        }
    }
    public override void FixedUpdate()
    {
        
    }

}
