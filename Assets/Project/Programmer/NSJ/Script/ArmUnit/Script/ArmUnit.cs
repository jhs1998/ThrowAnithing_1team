using TMPro.EditorUtilities;
using UnityEngine;

public class ArmUnit : ScriptableObject
{
    protected enum Type { Throw, Melee, Special ,Size}
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
    public void Init(PlayerController player)
    {
        Player = player; 
        _types = new ArmAttackType[(int)Type.Size];
        _types[(int)Type.Throw] = Instantiate(_throwAttack);
        _types[(int)Type.Melee] = Instantiate(_meleeAttack);
        _types[(int)Type.Special] = Instantiate(_specialAttack);

        _types[(int)Type.Throw]?.Init(Player);
        _types[(int)Type.Melee]?.Init(Player);
        _types[(int)Type.Special]?.Init(Player);
        _throwAttack = _types[(int)Type.Throw] as ArmThrowAttack;
        _meleeAttack = _types[(int)Type.Melee] as ArmMeleeAttack;
        _specialAttack = _types[(int)Type.Special] as ArmSpecialAttack;
    }
    public virtual void Enter() 
    {
        switch (Player.CurState)
        {
            case PlayerController.State.ThrowAttack:
                _types[(int)Type.Throw].Enter();
                break;
            case PlayerController.State.MeleeAttack:
                _types[(int)Type.Melee].Enter();
                break;
            case PlayerController.State.SpecialAttack:
                _types[(int)Type.Special].Enter();
                break;
        }
    }
    public virtual void Exit() 
    {
        switch (Player.CurState)
        {
            case PlayerController.State.ThrowAttack:
                _types[(int)Type.Throw].Exit();
                break;
            case PlayerController.State.MeleeAttack:
                _types[(int)Type.Melee].Exit();
                break;
            case PlayerController.State.SpecialAttack:
                _types[(int)Type.Special].Exit();
                break;
        }
    }
    public virtual void Update() 
    {
        switch (Player.CurState)
        {
            case PlayerController.State.ThrowAttack:
                _types[(int)Type.Throw].Update();
                break;
            case PlayerController.State.MeleeAttack:
                _types[(int)Type.Melee].Update();
                break;
            case PlayerController.State.SpecialAttack:
                _types[(int)Type.Special].Update();
                break;
        }
    }
    public virtual void FixedUpdate() 
    {
        switch (Player.CurState)
        {
            case PlayerController.State.ThrowAttack:
                _types[(int)Type.Throw].FixedUpdate();
                break;
            case PlayerController.State.MeleeAttack:
                _types[(int)Type.Melee].FixedUpdate();
                break;
            case PlayerController.State.SpecialAttack:
                _types[(int)Type.Special].FixedUpdate();
                break;
        }
    }
    public virtual void OnDrawGizmos() 
    {
        switch (Player.CurState)
        {
            case PlayerController.State.ThrowAttack:
                _types[(int)Type.Throw].OnDrawGizmos();
                break;
            case PlayerController.State.MeleeAttack:
                _types[(int)Type.Melee].OnDrawGizmos();
                break;
            case PlayerController.State.SpecialAttack:
                _types[(int)Type.Special].OnDrawGizmos();
                break;
        }
    }
    public virtual void OnTrigger() 
    {
        switch (Player.CurState)
        {
            case PlayerController.State.ThrowAttack:
                _types[(int)Type.Throw].OnTrigger();
                break;
            case PlayerController.State.MeleeAttack:
                _types[(int)Type.Melee].OnTrigger();
                break;
            case PlayerController.State.SpecialAttack:
                _types[(int)Type.Special].OnTrigger();
                break;
        }
    }
    public virtual void EndAnimation() 
    {
        switch (Player.CurState)
        {
            case PlayerController.State.ThrowAttack:
                _types[(int)Type.Throw].EndAnimation();
                break;
            case PlayerController.State.MeleeAttack:
                _types[(int)Type.Melee].EndAnimation();
                break;
            case PlayerController.State.SpecialAttack:
                _types[(int)Type.Special].EndAnimation();
                break;
        }
    }
    public virtual void OnCombo() 
    {
        switch (Player.CurState)
        {
            case PlayerController.State.ThrowAttack:
                _types[(int)Type.Throw].OnCombo();
                break;
            case PlayerController.State.MeleeAttack:
                _types[(int)Type.Melee].OnCombo();
                break;
            case PlayerController.State.SpecialAttack:
                _types[(int)Type.Special].OnCombo();
                break;
        }
    }
    public virtual void EndCombo() 
    {
        switch (Player.CurState)
        {
            case PlayerController.State.ThrowAttack:
                _types[(int)Type.Throw].EndCombo();
                break;
            case PlayerController.State.MeleeAttack:
                _types[(int)Type.Melee].EndCombo();
                break;
            case PlayerController.State.SpecialAttack:
                _types[(int)Type.Special].EndCombo();
                break;
        }
    }
}
