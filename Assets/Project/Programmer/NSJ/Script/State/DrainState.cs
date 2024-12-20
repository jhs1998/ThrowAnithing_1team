using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainState : PlayerState
{
    private GameObject _drainField => Player.DrainField;
    private float _drainDistance => Model.DrainDistance * 2;

    public DrainState(PlayerController controller) : base(controller)
    {
        _drainField.SetActive(false);
    }

    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;
        View.SetBool(PlayerView.Parameter.Drain, true);
        _drainField.SetActive(true);
        _drainField.transform.localScale = new Vector3(_drainDistance, _drainField.transform.localScale.y, _drainDistance);
    }

    public override void Update()
    {
        CheckInput();
    }

    public override void Exit()
    {
        View.SetBool(PlayerView.Parameter.Drain, false);
        Player.DrainField.SetActive(false);
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, Model.DrainDistance);
    }

    private void CheckInput()
    {
        //드레인 키를 뗐을 때
        if (Input.GetKeyUp(KeyCode.Z))
        {
            ChangeState(PlayerController.State.Idle);
        }
    }
}
