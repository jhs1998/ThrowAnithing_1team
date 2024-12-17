using System.Collections;
using UnityEngine;

public class JumpState : PlayerState
{
    private float _jumpPower;

    Coroutine _jumpRoutine;
    public JumpState(PlayerController controller) : base(controller)
    {
        View.OnJumpEvent += Jump;
        _jumpPower = controller.Model.JumpPower;
    }

    public override void Enter()
    {

        if (_jumpRoutine == null)
        {
            _jumpRoutine = CoroutineHandler.StartRoutine(JumpRoutine());
        }
    }
    public override void Exit()
    {
        if (_jumpRoutine != null)
        {
            CoroutineHandler.StopRoutine(_jumpRoutine);
            _jumpRoutine = null;
        }
    }

    public override void FixedUpdate()
    {

    }

    private void Jump()
    {
        Rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
    }

    IEnumerator JumpRoutine()
    {
        View.SetTrigger(PlayerView.Parameter.Jump);
        while (true)
        {
            if (Rb.velocity.y < 0)
            {
                if (Physics.Raycast(transform.position, Vector3.down, 1f))
                {
                    // TODO: 지면 체크 로직
                    View.SetTrigger(PlayerView.Parameter.Landing);
                    Player.ChangeState(PlayerController.State.Idle);
                    yield break;
                }
            }
            yield return 0.02f.GetDelay();
        }
    }
}
