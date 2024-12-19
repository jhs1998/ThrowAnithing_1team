using System.Collections;
using UnityEngine;

public class FallState : PlayerState
{

    private Vector3 _inertia; // 관성력

    private bool _isDoubleJump;

    Coroutine _fallRoutine;
    Coroutine _checkInputRoutine;
    public FallState(PlayerController controller) : base(controller)
    {
    }
    public override void Enter()
    {
        if (Player.PrevState != PlayerController.State.Jump)
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

    public override void OnDrawGizmos()
    {
        Vector3 CheckPos = new Vector3(transform.position.x, transform.position.y + 0.31f, transform.position.z);
        if (Physics.SphereCast(CheckPos, 0.3f, Vector3.down, out RaycastHit hit, 1f))
        {
            Gizmos.DrawLine(CheckPos, hit.point);
            Gizmos.DrawWireSphere(CheckPos + Vector3.down * hit.distance, 0.3f);
        }
    }

    IEnumerator FallRoutine()
    {
        // Fall 상태에서 받을 수 있는 입력 대기
        if (_checkInputRoutine == null)
            _checkInputRoutine = CoroutineHandler.StartRoutine(CheckInputRoutine());

        yield return 0.1f.GetDelay();
        while (Player.IsGround == false)
        {
            // 관성 유지, 벽과 접촉시 이동안함
            if (Player.IsWall == false)
            {
                Rb.velocity = new Vector3(_inertia.x, Rb.velocity.y, _inertia.z);
            }
            else
            {
                Rb.velocity = new Vector3(0, Rb.velocity.y, 0);
            }
            // 떨어지기 시작했을때 지면과 충분히 가까워졌다면 지면 체크 로직 종료
            if (Rb.velocity.y < 0)
            {
                Vector3 CheckPos = new Vector3(transform.position.x, transform.position.y + 0.31f, transform.position.z);
                if (Physics.SphereCast(CheckPos, 0.3f, Vector3.down, out RaycastHit hit, 1f))
                    break;
            }
            yield return 0.02f.GetDelay();
        }

        // 착지 애니메이션 실행
        View.SetTrigger(PlayerView.Parameter.Landing);

        while (true)
        {
            // 착지 애니메이션이 끝났을때 Idle 모드로 전환
            if (View.GetIsAnimFinish(PlayerView.Parameter.Landing) == true)
            {
                _isDoubleJump = false;
                ChangeState(PlayerController.State.Idle);
                break;
            }
            yield return null;
        }
    }

    IEnumerator CheckInputRoutine()
    {
        while (true)
        {
            if (Input.GetButtonDown("Jump") && _isDoubleJump == false)
            {
                _isDoubleJump = true;
                ChangeState(PlayerController.State.Jump);
                break;
            }
            yield return null;
        }
        _checkInputRoutine = null;
    }
}
