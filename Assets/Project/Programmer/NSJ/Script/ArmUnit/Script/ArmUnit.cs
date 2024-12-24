using System;
using TMPro.EditorUtilities;
using UnityEngine;

public class ArmUnit : ScriptableObject
{
    protected enum Type { Throw, Melee, Special, JumpDown,Size}
    [HideInInspector] public PlayerController Player;
    public PlayerModel Model => Player.Model;
    public PlayerView View => Player.View;
    protected Transform transform => Player.transform;
    protected GameObject gameObject => Player.gameObject;
    protected Transform _muzzlePoint => Player.MuzzletPoint;

    protected ArmAttackType[] _types;
    [SerializeField]protected ArmThrowAttack _throwAttack;
    [SerializeField]protected ArmMeleeAttack _meleeAttack;
    [SerializeField]protected ArmSpecialAttack _specialAttack;
    [SerializeField] protected ArmJumpDown _jumpDown;
    public void Init(PlayerController player)
    {
        Player = player; 
        _types = new ArmAttackType[(int)Type.Size];
        _types[(int)Type.Throw] = Instantiate(_throwAttack);
        _types[(int)Type.Melee] = Instantiate(_meleeAttack);
        _types[(int)Type.Special] = Instantiate(_specialAttack);
        _types[(int)Type.JumpDown] = Instantiate(_jumpDown);

        _types[(int)Type.Throw]?.Init(Player);
        _types[(int)Type.Melee]?.Init(Player);
        _types[(int)Type.Special]?.Init(Player);
        _types[(int)Type.JumpDown]?.Init(Player);
        _throwAttack = _types[(int)Type.Throw] as ArmThrowAttack;
        _meleeAttack = _types[(int)Type.Melee] as ArmMeleeAttack;
        _specialAttack = _types[(int)Type.Special] as ArmSpecialAttack;
        _jumpDown = _types[(int)Type.JumpDown] as ArmJumpDown;
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
        }
        return attackType;
    }
}
