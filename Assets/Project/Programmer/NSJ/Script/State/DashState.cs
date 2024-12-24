using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    private Vector3 _moveDir;
    public DashState(PlayerController controller) : base(controller)
    {
        UseStamina = true;
    }

    public override void Enter()
    {
        InputKey();
        View.SetTrigger(PlayerView.Parameter.Dash);
    }

    public override void Update()
    {
        Dash();
    }
    public override void EndAnimation()
    {
        ChangeState(PlayerController.State.Idle);
    }
    /// <summary>
    /// ´ë½¬
    /// </summary>
    public void Dash()
    {

        Player.LookAtMoveDir(_moveDir);

        Rb.velocity = transform.forward * Model.DashPower;
    }

    private void InputKey()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        _moveDir = new Vector3(x, 0, z);
    }
}
