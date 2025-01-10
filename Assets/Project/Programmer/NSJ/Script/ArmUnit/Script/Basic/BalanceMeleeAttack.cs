using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Balance PrevMelee", menuName = "Arm/AttackType/Balance/PrevMelee")]
public class BalanceMeleeAttack : ArmMeleeAttack
{
    [Header("���׹̳� �Ҹ�")]
    public float StaminaAmount;
    [SerializeField] float _range;
    [SerializeField] int _damage;
    [Range(0,180)][SerializeField] float _angle;
    [SerializeField] float _damageMultiplier;

    private float _staminaReduction => 1 - Model.StaminaReduction / 100;
    Coroutine _meleeRoutine;
    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;

        // ù ���� �� ù ���� �ִϸ��̼� ����
        if (Player.PrevState != PlayerController.State.MeleeAttack)
        {
            View.SetTrigger(PlayerView.Parameter.BalanceMelee);
        }
        else
        {
            View.SetTrigger(PlayerView.Parameter.OnCombo);
        }

        if (Player.IsAttackFoward == true)
        {
            // ���ݹ��� �ٶ󺸱�
            Player.LookAtAttackDir();
        }
    }

    public override void Update()
    {
        //Debug.Log("PrevMelee");
    }

    public override void Exit()
    {
        if (_meleeRoutine != null)
        {
            CoroutineHandler.StopRoutine(_meleeRoutine);
            _meleeRoutine = null;
        }
    }
    public override void OnTrigger()
    {
        AttackMelee();
    }
    /// <summary>
    /// ���� ����
    /// </summary>
    public void AttackMelee()
    {
        // ���� �տ� �ִ� ���͵��� Ȯ���ϰ� �ǰ� ����
        // 1. ���濡 �ִ� ���� Ȯ��
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + Player.AttackHeight, transform.position.z);
        Vector3 attackPos = playerPos;
        int hitCount = Physics.OverlapSphereNonAlloc(attackPos, _range, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            // 2. ���� ���� �ִ��� Ȯ��
            Vector3 source = transform.position;
            source.y = 0;
            Vector3 destination = Player.OverLapColliders[i].transform.position;
            destination.y = 0;

            Vector3 targetDir = (destination - source).normalized;
            float targetAngle = Vector3.Angle(transform.forward, targetDir); // ��ũ�ڻ��� �ʿ� (������)
            if (targetAngle > _angle * 0.5f)
                continue;
            
            int finalDamage = Player.GetFinalDamage(_damage,_damageMultiplier, out bool isCritical);
            // ������ �ֱ�
            Battle.TargetAttackWithDebuff(Player.OverLapColliders[i], isCritical, finalDamage,  false);
            Battle.TargetCrowdControl(Player.OverLapColliders[i],CrowdControlType.Stiff);
        }
    }

    public override void OnCombo()
    {
        if (_meleeRoutine == null)
        {
            _meleeRoutine = CoroutineHandler.StartRoutine(OnComboRoutine());
        }
    }

    public override void EndCombo()
    {
        if (_meleeRoutine != null)
        {
            ChangeState(PlayerController.State.Idle);
        }

    }



    IEnumerator OnComboRoutine()
    {
        while (true)
        {
            if (Player.CurState != PlayerController.State.MeleeAttack)
                yield break;

            if (InputKey.GetButtonDown(InputKey.Melee))
            {
                ChangeState(PlayerController.State.MeleeAttack);
            }
            else if (InputKey.GetButtonDown(InputKey.Throw))
            {
                ChangeState(PlayerController.State.ThrowAttack);
            }
            yield return null;
        }
    }

    public override void OnDrawGizmos()
    {
        if (Model == null)
            return;
        //�Ÿ�
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + Player.AttackHeight, transform.position.z);
        Vector3 attackPos = playerPos;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos, _range);

        //����
        Vector3 rightDir = Quaternion.Euler(0, _angle * 0.5f, 0) * transform.forward;
        Vector3 leftDir = Quaternion.Euler(0, _angle * -0.5f, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + rightDir * _range);
        Gizmos.DrawLine(transform.position, transform.position + leftDir * _range);
    }
}