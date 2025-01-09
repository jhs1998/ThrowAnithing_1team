using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeAttackState : PlayerState
{
    Coroutine _meleeRoutine;
    public MeleeAttackState(PlayerController controller) : base(controller)
    {
        UseStamina = true;
        StaminaAmount = Model.MeleeAttackStamina[0];
    }
    public override void InitArm()
    {
        if(Model.NowWeapon == GlobalGameData.AmWeapon.Balance)
        {
            BalanceMeleeAttack meleeAttack = Arm.MeleeAttack as BalanceMeleeAttack;
            StaminaAmount = meleeAttack.StaminaAmount;
        }
        else if (Model.NowWeapon == GlobalGameData.AmWeapon.Power)
        {
            StaminaAmount = Model.MeleeAttackStamina[0];
        }      
    }
    public override void Enter() => Arm.Enter();
    public override void Exit() => Arm.Exit();
    public override void Update() => Arm.Update();
    public override void FixedUpdate() => Arm.FixedUpdate();
    public override void OnDrawGizmos() => Arm.OnDrawGizmos();
    public override void OnTrigger() => Arm.OnTrigger();
    public override void EndAnimation() => Arm.EndAnimation();
    public override void OnCombo() => Arm.OnCombo();
    public override void EndCombo() => Arm.EndCombo();

    public override void OnMove(Vector3 value) => Arm.OnMove(value);
    public override void OnJump() => Arm.OnJump();
    public override void OnRanged_Attack() => Arm.OnRanged_Attack();
    public override void OnSpecial_Attack() => Arm.OnSpecial_Attack();
    public override void OnMelee_Attack() => Arm.OnMelee_Attack();
    public override void OnLoak_On()=> Arm.OnLoak_On();
    public override void OnLoak_Off() => Arm.OnLoak_Off();
    public override void OnDash() => Arm.OnDash();
    public override void OnInteraction() => Arm.OnInteraction();
    public override void OnDrain() => Arm.OnDrain();
    public override void OnOpen_Settine() => Arm.OnOpen_Settine();
    public override void OnInvenOpen() => Arm.OnInvenOpen();
}
