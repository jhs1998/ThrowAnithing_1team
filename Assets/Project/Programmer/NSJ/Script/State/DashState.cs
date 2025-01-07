using UnityEngine;

public class DashState : PlayerState
{

    float _timeBuffer;
    Coroutine _checkInputRoutine;
    public DashState(PlayerController controller) : base(controller)
    {
        UseStamina = true;
        StaminaAmount = Model.DashStamina;
    }

    public override void Enter()
    {
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
        Rb.velocity = transform.forward * Model.DashDistance;
    }
}
