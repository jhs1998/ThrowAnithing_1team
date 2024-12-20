using Assets.Project.Programmer.NSJ.RND.Script.State;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.XR;
using Zenject.SpaceFighter;

public class PlayerState : BaseState
{
    public bool UseStamina;

    protected PlayerController Player;

    protected GameObject gameObject;
    protected Transform transform;
    protected PlayerModel Model;
    protected PlayerView View;
    protected Rigidbody Rb;
    public PlayerState(PlayerController controller)
    {
        Player = controller;
        gameObject = controller.gameObject;
        transform = controller.transform;
        Model = controller.Model;
        View = controller.View;
        Rb = controller.Rb;
    }

    public virtual void OnDash() { }
    public virtual void OnThrowAttack() { }
    public virtual void OnCombo() { }
    public virtual void EndCombo() { }
    protected void ChangeState(PlayerController.State state)
    {
        Player.ChangeState(state);
    }
}
