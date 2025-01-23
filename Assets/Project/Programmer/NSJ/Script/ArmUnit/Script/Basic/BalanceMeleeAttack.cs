using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Balance PrevMelee", menuName = "Arm/AttackType/Balance/PrevMelee")]
public class BalanceMeleeAttack : ArmMeleeAttack
{
    [SerializeField] GameObject _attackEffect;
    [Header("���׹̳� �Ҹ�")]
    public float StaminaAmount;
    [SerializeField] float _range;
    [SerializeField] int _damage;
    [Range(0, 180)][SerializeField] float _angle;
    [SerializeField] float _damageMultiplier;
    [SerializeField] private float _coolTime;
    private float _staminaReduction => 1 - Model.StaminaReduction / 100;

    private bool _isCooltime;
    Coroutine _meleeRoutine;
    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;

        if (_isCooltime == true)
        {
            Model.CurStamina += StaminaAmount;

            ChangeState(Player.PrevState);

            return;
        }


        View.SetTrigger(PlayerView.Parameter.BalanceMelee);



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

            int finalDamage = Player.GetFinalDamage(_damage, _damageMultiplier, out bool isCritical);

            // ����
            SoundManager.PlaySFX(isCritical == true ? Player.Sound.Hit.Critical : Player.Sound.Hit.Hit);

            // ������ �ֱ�
            Battle.TargetAttackWithDebuff(Player.OverLapColliders[i], isCritical, finalDamage, false);
            Battle.TargetCrowdControl(Player.OverLapColliders[i], CrowdControlType.Stiff);
        }


        CoroutineHandler.StartRoutine(CooltimeRoutine(_coolTime));

        ObjectPool.GetPool(_attackEffect, Player.MeleeAttackPoint.transform.position, transform.rotation, 2f);
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


            if (InputKey.GetButtonDown(InputKey.Throw))
            {
                ChangeState(PlayerController.State.ThrowAttack);
                _meleeRoutine = null;
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator CooltimeRoutine(float coolTime)
    {
        _isCooltime = true;
        yield return coolTime.GetDelay();
        _isCooltime = false;
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
