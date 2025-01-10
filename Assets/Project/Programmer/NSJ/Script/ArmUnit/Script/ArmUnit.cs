using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class ArmUnit : ScriptableObject
{
    [SerializeField] public string Name;
    [SerializeField] public GlobalGameData.AmWeapon ArmType;
    protected enum Type { Throw, Melee, Special, JumpDown,JumpAttack,Size}
    [HideInInspector] public PlayerController Player;
    public PlayerModel Model => Player.Model;
    public PlayerView View => Player.View;
    protected Transform transform => Player.transform;
    protected GameObject gameObject => Player.gameObject;
    protected Transform _muzzlePoint => Player.MuzzletPoint;

    [SerializeField] protected AttackTypeStruct _attackType;
    [SerializeField] protected ArmAttackType[] _types;

    [System.Serializable]
    protected struct AttackTypeStruct
    {
       public ArmThrowAttack ThrowAttack;
       public ArmMeleeAttack MeleeAttack;
       public ArmSpecialAttack SpecialAttack;
       public ArmJumpDown JumpDown;
       public ArmJumpAttack JumpAttack;
    }
    public ArmThrowAttack ThrowAttack { get { return _attackType.ThrowAttack; } set { _attackType.ThrowAttack = value; } }
    public ArmMeleeAttack MeleeAttack { get { return _attackType.MeleeAttack; } set { _attackType.MeleeAttack = value; } }
    public ArmSpecialAttack SpecialAttack { get { return _attackType.SpecialAttack; } set { _attackType.SpecialAttack = value; } }
    public ArmJumpDown JumpDown { get { return _attackType.JumpDown; } set { _attackType.JumpDown = value; } }
    public ArmJumpAttack JumpAttack { get { return _attackType.JumpAttack; } set { _attackType.JumpAttack = value; } }
    public virtual void Init(PlayerController player)
    {
        Player = player; 
        _types = new ArmAttackType[(int)Type.Size];
    }



    public virtual void Enter()
    {
        SelectType().Enter();
    }
    public virtual void Exit()
    {
        SelectType().Exit();
    }
    public virtual void Update()
    {
        SelectType().Update();
    }
    public virtual void FixedUpdate()
    {
        SelectType().FixedUpdate();
    }
    public virtual void OnDrawGizmos()
    {
        SelectType().OnDrawGizmos();
    }
    public virtual void OnTrigger()
    {
        SelectType().OnTrigger();
    }
    public virtual void EndAnimation()
    {
        SelectType().EndAnimation();
    }
    public virtual void OnCombo()
    {
        SelectType().OnCombo();
    }
    public virtual void EndCombo()
    {
        SelectType().EndCombo();
    }
    private ArmAttackType SelectType()
    {
        ArmAttackType attackType = null;
        switch (Player.CurState)
        {
            case PlayerController.State.ThrowAttack:
                attackType = _types[(int)Type.Throw];
                break;
            case PlayerController.State.MeleeAttack:
                attackType = _types[(int)Type.Melee];
                break;
            case PlayerController.State.SpecialAttack:
                attackType = _types[(int)Type.Special];
                break;
            case PlayerController.State.JumpDown:
                attackType = _types[(int)Type.JumpDown];
                break;
            case PlayerController.State.JumpAttack:
                attackType = _types[(int)Type.JumpAttack];
                break;
        }
        return attackType;
    }
    protected void InitType(Type type, ArmAttackType armAttackType)
    {
        _types[(int)type] = Instantiate(armAttackType);
        _types[(int)type]?.Init(Player);
    }
    protected void InitAllType()
    {
        InitType(Type.Throw, ThrowAttack);
        InitType(Type.Melee, MeleeAttack);
        InitType(Type.Special, SpecialAttack);
        InitType(Type.JumpDown, JumpDown);
        InitType(Type.JumpAttack, JumpAttack);
    }
}
