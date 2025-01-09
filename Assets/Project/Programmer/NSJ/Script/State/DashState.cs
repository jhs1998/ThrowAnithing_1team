using System.Collections;
using UnityEngine;

public class DashState : PlayerState
{
    float _timeBuffer;
    bool _isDashEnd;
    Vector3 _startPos;
    Coroutine _checkInputRoutine;
    public DashState(PlayerController controller) : base(controller)
    {
        UseStamina = true;
       
    }
    public override void InitArm()
    {
        StaminaAmount = Model.DashStamina;
    }
    public override void Enter()
    {
        _isDashEnd = false;

        _startPos = transform.position;
        Player.IsInvincible = true;
        Player.LookAtMoveDir();
        View.SetTrigger(PlayerView.Parameter.Dash);
    }
    public override void Exit()
    {
        Player.IsInvincible = false;
    }
    public override void Update()
    {
        Dash();

        _timeBuffer += Time.deltaTime;
        if (InputKey.GetButtonDown(InputKey.Dash))
        {
            _timeBuffer = 0;
        }
    }
    public override void OnTrigger() { }
    public override void EndAnimation()
    {
        if(_timeBuffer < 0.25f)
        {
            ChangeState(PlayerController.State.Dash);
        }
        else if (Player.IsGround != true)
        {
            Rb.velocity /= 2;
            if (Player.IsDoubleJump == false)
                ChangeState(PlayerController.State.Fall);
            else
                ChangeState(PlayerController.State.DoubleJumpFall);
        }
        else
        {
            ChangeState(PlayerController.State.Idle);
        }

    }
    /// <summary>
    /// ´ë½¬
    /// </summary>
    public void Dash()
    {
        if (Vector3.Distance(transform.position, _startPos) < Model.DashDistance)
        {
            Rb.velocity = transform.forward * Model.MoveSpeed * 2;
        }
        else if(_isDashEnd == false)
        {
            _isDashEnd = true;
            View.SetTrigger(PlayerView.Parameter.DashEnd);
            Rb.velocity = transform.forward * Model.MoveSpeed;
        }
    }

    IEnumerator DashEndRoutine()
    {
        float speed = Model.MoveSpeed * 2;
        while (true)
        {
            Rb.velocity = transform.forward * speed;
            speed -= Time.deltaTime * 5f;
            if(speed < 0)
            {
                yield break;
            }
            yield return null;
        }
    }
}
