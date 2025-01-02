using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpFallState : PlayerState
{
    private Vector3 _inertia; // ������

    Coroutine _fallRoutine;
    Coroutine _checkInputRoutine;
    public DoubleJumpFallState(PlayerController controller) : base(controller)
    {
    }
    public override void Enter()
    {
        if (Player.PrevState != PlayerController.State.Jump &&
            Player.PrevState != PlayerController.State.DoubleJump)
        {
            View.SetTrigger(PlayerView.Parameter.Fall);
        }

        _inertia = new Vector3(Rb.velocity.x, Rb.velocity.y, Rb.velocity.z);
        if (_fallRoutine == null)
        {
            _fallRoutine = CoroutineHandler.StartRoutine(FallRoutine());
        }
    }

    public override void Exit()
    {
        if (_fallRoutine != null)
        {
            CoroutineHandler.StopRoutine(_fallRoutine);
            _fallRoutine = null;
        }

        if (_checkInputRoutine != null)
        {
            CoroutineHandler.StopRoutine(_checkInputRoutine);
            _checkInputRoutine = null;
        }
    }

    public override void FixedUpdate()
    {
        CheckIsNearGround();
    }
    public override void OnDrawGizmos()
    {
        Vector3 CheckPos = new Vector3(transform.position.x, transform.position.y + 0.31f, transform.position.z);
        if (Physics.SphereCast(CheckPos, 0.3f, Vector3.down, out RaycastHit hit, 1, Layer.GetLayerMaskEveryThing(), QueryTriggerInteraction.Ignore))
        {
            Gizmos.DrawLine(CheckPos, hit.point);
            Gizmos.DrawWireSphere(CheckPos + Vector3.down * hit.distance, 0.3f);
        }
    }
    public override void EndAnimation()
    {
        Player.IsDoubleJump = false;
        Player.IsJumpAttack = false;

        ChangeState(PlayerController.State.Idle);
    }

    IEnumerator FallRoutine()
    {
        // Fall ���¿��� ���� �� �ִ� �Է� ���
        if (_checkInputRoutine == null)
            _checkInputRoutine = CoroutineHandler.StartRoutine(CheckInputRoutine());

        yield return 0.1f.GetDelay();
        while (Player.IsGround == false)
        {
            // ���� ����, ���� ���˽� �̵�����
            if (Player.IsWall == false)
            {
                Rb.velocity = new Vector3(_inertia.x, Rb.velocity.y, _inertia.z);
            }
            else
            {
                Rb.velocity = new Vector3(0, Rb.velocity.y, 0);
            }
            // �������� ���������� ����� ����� ��������ٸ� ���� üũ ���� ����
            if (Rb.velocity.y < 0)
            {
                Vector3 CheckPos = new Vector3(transform.position.x, transform.position.y + 0.31f, transform.position.z);
                if (CheckIsNearGround() && Rb.velocity.y < 0)
                    break;
            }
            yield return 0.02f.GetDelay();
        }

        if (_checkInputRoutine != null)
        {
            CoroutineHandler.StopRoutine(_checkInputRoutine);
            _checkInputRoutine = null;
        }
        // ���� �ִϸ��̼� ����
        View.SetTrigger(PlayerView.Parameter.Landing);
    }

    IEnumerator CheckInputRoutine()
    {
        while (true)
        {
            if (InputKey.GetButtonDown(InputKey.Jump) && Player.IsDoubleJump == false)
            {
                Player.IsDoubleJump = true;
                ChangeState(PlayerController.State.DoubleJump);
                break;
            }
            if (InputKey.GetButtonDown(InputKey.Throw) && Player.IsJumpAttack == false)
            {
                Debug.Log(1231);
                Player.IsJumpAttack = true;
                ChangeState(PlayerController.State.JumpAttack);
                break;
            }
            if (InputKey.GetButtonDown(InputKey.Melee) && Player.IsDoubleJump == true)
            {
                Player.IsDoubleJump = false;
                ChangeState(PlayerController.State.JumpDown);
                break;
            }
            yield return null;
        }
        _checkInputRoutine = null;
    }

    /// <summary>
    /// ���鿡 ������� üũ
    /// </summary>
    private bool CheckIsNearGround()
    {
        Vector3 CheckPos = new Vector3(transform.position.x, transform.position.y + 0.31f, transform.position.z);
        if (Physics.SphereCast(CheckPos, 0.3f, Vector3.down, out RaycastHit hit, 1f, Layer.GetLayerMaskEveryThing(), QueryTriggerInteraction.Ignore))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}