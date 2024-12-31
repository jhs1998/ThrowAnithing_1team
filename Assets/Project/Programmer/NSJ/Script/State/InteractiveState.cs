using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class InteractiveState : PlayerState
{
    PlayerInteractor interactor;

    Coroutine _checkCoroutine;
    public InteractiveState(PlayerController controller) : base(controller)
    {
        CantChangeState= true;
        interactor = Player.GetComponent<PlayerInteractor>();
    }
    public override void Enter()
    {
        Rb.velocity = new Vector3(0,Rb.velocity.y,0);
    }

    public override void Exit()
    {
        
    }
    public override void Update()
    {
        if (interactor.IsInteractiveActive == false)
        {
            ChangeState(PlayerController.State.Idle);
        }
    }
}
