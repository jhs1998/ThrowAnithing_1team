using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAttackType : ScriptableObject
{
     public PlayerController Player;
    public PlayerModel Model => Player.Model;
    public PlayerView View => Player.View;
    protected Transform transform => Player.transform;
    protected GameObject gameObject => Player.gameObject;
    protected Transform _muzzlePoint => Player.MuzzletPoint;
    public virtual void Init(PlayerController player)
    {
        Player = player;
    }
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void OnDrawGizmos() { }
    public virtual void OnTrigger() { }
    public virtual void EndAnimation() { }
    public virtual void OnCombo() { }
    public virtual void EndCombo() { }
    protected void ChangeState(PlayerController.State state)
    {
        Player.ChangeState(state);
    }

}
