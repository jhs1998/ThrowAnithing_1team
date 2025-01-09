using Assets.Project.Programmer.NSJ.RND.Script.State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using Zenject.SpaceFighter;

public class PlayerState : BaseState
{
    public bool UseStamina;
    public float StaminaAmount;
    public bool CantChangeState;
    protected PlayerController Player;

    protected GameObject gameObject => Player.gameObject;
    protected Transform transform => Player.transform;
    protected PlayerModel Model =>Player.Model;
    protected PlayerView View => Player.View;
    protected ArmUnit Arm => Model.Arm;
    protected Rigidbody Rb => Player.Rb;
    protected Vector3 MoveDir => Player.MoveDir;
    public PlayerState(PlayerController controller)
    {
        Player = controller;
    }
    public virtual void OnTrigger() { }
    public virtual void EndAnimation() { }
    public virtual void OnCombo() { }
    public virtual void EndCombo() { }
    public virtual void TriggerCantOperate() { }
    public virtual void InitArm() { }
    protected void ChangeState(PlayerController.State state)
    {
        Player.ChangeState(state);
    }

    #region 인풋시스템 콜백
    public virtual void OnMove(Vector3 value) { }
    public virtual void OnJump() { }
    public virtual void OnRanged_Attack() { }
    public virtual void OnSpecial_Attack() { }
    public virtual void OnMelee_Attack() { }
    public virtual void OnLoak_On() { }
    public virtual void OnLoak_Off() { }
    public virtual void OnDash() { }
    public virtual void OnInteraction() { }
    public virtual void OnDrain() { }
    public virtual void OnOpen_Settine() { }
    public virtual void OnInvenOpen() { }
    #endregion
}
