using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Power PrevMelee", menuName = "Arm/AttackType/Power/PrevMelee")]
public class PowerMeleeAttack : ArmMeleeAttack
{
    [System.Serializable]
    struct ChargeStruct
    {
        [Header("���� �ð�")]
        public float ChargeTime;
        [Header("�߰� ������")]
        public int Damage;
        [Header("���� ��Ÿ�")]
        public float AttackRange;
        [Header("���� ���� ����")]
        [Range(0, 180)] public float AttackAngle;
        [Header("�˹� �Ÿ�")]
        public float KnockBackRange;
        [Header("���� �Ÿ�")]
        public float RushDistance;
        [HideInInspector] public float Stamina;

    }
    [System.Serializable]
    struct ChargeEffectStruct
    {
        public GameObject Charge;
        public GameObject Attack;
    }
    [System.Serializable]
    struct EffectStruct
    {
        public ChargeEffectStruct[] Charges;
        public GameObject Full;
    }
    [SerializeField] private ChargeStruct[] _charges;
    [SerializeField] private EffectStruct _effects;
 
    [SerializeField] private float RushSpeed;
    [SerializeField] private float _moveSpeedMultyPlier;
    [SerializeField] private float _autoAttackDelay;
    private float _staminaReduction => 1 - Model.StaminaReduction / 100;
    private float m_curChargeTime;
    float _autoAttackTime;
    bool _isAutoAttack;
    private float _curChargeTime
    {
        get { return m_curChargeTime; }
        set
        {
            m_curChargeTime = value;
            Model.CurStaminaCharge = m_curChargeTime;
            View.SetFloat(PlayerView.Parameter.Charge, m_curChargeTime);
        }
    }

    private GameObject _curChargeEffect;
    Coroutine _chargeRoutine;
    Coroutine _autoAttackRoutine;
    public override void Init(PlayerController player, ArmUnit arm)
    {
        base.Init(player, arm);
        for (int i = 0; i < _charges.Length; i++)
        {
            _charges[i].Damage = (int)Model.PowerMeleeAttack[i];
            _charges[i].Stamina = Model.MeleeAttackStamina[i];
        }
    }

    public override void Enter()
    {
        // ����� ���׹̳���ŭ �ٽ� ȸ��
        Model.CurStamina += Model.MeleeAttackStamina[0] * _staminaReduction;
        // �ִ� ���׹̳� �������� ����
        Model.MaxStaminaCharge = _charges[_charges.Length - 1].ChargeTime;
        // ��ġ ����
        Player.Rb.velocity = Vector3.zero;
        // ���׹̳� ȸ�� ����
        Player.CanStaminaRecovery = false;
        // ���ݹ��� �ٶ�
        Player.LookAtAttackDir();
        // �ִϸ��̼� ����
        View.SetTrigger(PlayerView.Parameter.PowerMelee);
        if (_chargeRoutine == null)
        {
            _chargeRoutine = CoroutineHandler.StartRoutine(ChargeRoutine());
        }
    }
    public override void Exit()
    {
        StopCoroutines();
        // ���׹̳� �ٽ� ȸ�� ����
        Player.CanStaminaRecovery = true;

        // ������ ���� ����Ʈ ����
        if (_curChargeEffect != null)
        {
            ObjectPool.ReturnPool(_curChargeEffect);
            _curChargeEffect = null;
        }

        // �ʱ�ȭ
        _curChargeTime = 0;
        _index = 0;
        _isAutoAttack = false;
        Player.IsInvincible = false;

        // ���� ���� ����
        Player.StopSFX();

    }
    public override void Update()
    {

    }

    public override void OnTrigger()
    {
        AttackMelee();
        // ������ ���� ����Ʈ ����
        if (_curChargeEffect != null)
        {
            ObjectPool.ReturnPool(_curChargeEffect);
            _curChargeEffect = null;
        }
    }
    public override void EndCombo()
    {
        ChangeState(PlayerController.State.Idle);
    }

    IEnumerator ChargeRoutine()
    {
        _index = 0;

        // ���� ����
        Player.PlaySFX(Player.Sound.Power.Charge);
        while (true)
        {
            Move();
            ProcessCharge();

            if (InputKey.GetButtonUp(InputKey.Melee))
            {
                ChargeEnd();
                break;
            }
            yield return null;
        }
    }

    private void ProcessCharge()
    {
        if (_autoAttackRoutine != null)
            return;
        if (_isAutoAttack == false)
        {
            // �����ð� ���
            _curChargeTime += Time.deltaTime * View.GetFloat(PlayerView.Parameter.AttackSpeed);
        }
        if (_charges.Length > _index + 1)
        {
            // ���׹̳��� �����ϸ� ���� ����
            if (Model.CurStamina < _charges[_index + 1].Stamina)
            {
                ProcessAutoAttackTmer();
                return;
            }
            // ���� �ð��� ���� �ܰ�� �Ѿ �� ���� ��
            if (_curChargeTime > _charges[_index + 1].ChargeTime)
            {
                _index++;
                _autoAttackTime = 0;
                ShowArmEffect();
            }

        }
        else
        {
            ProcessAutoAttackTmer();
        }

    }
    public void AttackMelee()
    {
        // �ڿ��Ҹ� ó��
        Model.CurStamina -= _charges[_index].Stamina * _staminaReduction;

        // ĳ���� ���� ���� �̵�
        CoroutineHandler.StartRoutine(RushRoutine(transform.forward, _charges[_index].RushDistance));
        // ���� �տ� �ִ� ���͵��� Ȯ���ϰ� �ǰ� ����
        // 1. ���濡 �ִ� ���� Ȯ��
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + Player.AttackHeight, transform.position.z);
        Vector3 attackPos = playerPos;
        int hitCount = Physics.OverlapSphereNonAlloc(attackPos, _charges[_index].AttackRange, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            Transform targetTransform = Player.OverLapColliders[i].transform;
            // 2. ���� ���� �ִ��� Ȯ��
            Vector3 source = transform.position;
            source.y = 0;
            Vector3 destination = targetTransform.position;
            destination.y = 0;

            Vector3 targetDir = (destination - source).normalized;
            float targetAngle = Vector3.Angle(transform.forward, targetDir); // ��ũ�ڻ��� �ʿ� (������)
            if (targetAngle > _charges[_index].AttackAngle * 0.5f)
                continue;


            int attackDamage = Player.GetFinalDamage(_charges[_index].Damage, out bool isCritical);
            Battle.TargetAttackWithDebuff(Player.OverLapColliders[i], isCritical, attackDamage, false);
            Battle.TargetCrowdControl(Player.OverLapColliders[i], CrowdControlType.Stiff);

            // ����
            SoundManager.PlaySFX(isCritical == true? Player.Sound.Hit.Critical: Player.Sound.Hit.Hit);

            if (_charges[_index].KnockBackRange > 0)
            {
                // �������� �б�
                Player.DoKnockBack(targetTransform, transform.forward, _charges[_index].KnockBackRange);
                // �÷��̾� �߽� �б�
                //Player.DoKnockBack(targetTransform, transform, _charges[_index].KnockBackRange);
            }

            if (_index == 0)
                break;
        }

        ObjectPool.GetPool(_effects.Charges[_index].Attack, Player.MeleeAttackPoint.transform.position, transform.rotation, 2f);

    }
    public override void OnDrawGizmos()
    {
        if (Model == null)
            return;
        //�Ÿ�
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + Player.AttackHeight, transform.position.z);
        Vector3 attackPos = playerPos;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos, _charges[_index].AttackRange);

        //����
        Vector3 rightDir = Quaternion.Euler(0, _charges[_index].AttackAngle * 0.5f, 0) * transform.forward;
        Vector3 leftDir = Quaternion.Euler(0, _charges[_index].AttackAngle * -0.5f, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + rightDir * _charges[_index].AttackRange);
        Gizmos.DrawLine(transform.position, transform.position + leftDir * _charges[_index].AttackRange);
    }

    // ������ �� ����Ʈ ��Ÿ����
    private void ShowArmEffect()
    {
        if (_curChargeEffect != null)
        {
            ObjectPool.ReturnPool(_curChargeEffect);
        }
        // ������ ����Ʈ
        _curChargeEffect = ObjectPool.GetPool(_effects.Charges[_index].Charge, Player.RightArmPoint);
    }

    private void ChargeEnd()
    {
        StopCoroutines();
        Player.IsInvincible = true;
        _autoAttackTime = 0;


        _chargeRoutine = null;
        // ���ݹ��� �ٶ󺸱�
        Player.LookAtAttackDir();
        // �ִϸ��̼� ����
        View.SetTrigger(PlayerView.Parameter.ChargeEnd);

        // ���� ���� ����
        Player.StopSFX();
    }

    IEnumerator RushRoutine(Vector3 rushDir, float rushDistance)
    {
        // Ǯ������(���� ������ ��) ���� ����Ʈ
        if (rushDistance > 0)
        {
            ObjectPool.GetPool(_effects.Full, Player.DashFrountPoint, 2f);
        }


        Vector3 originPos = transform.position;

        while (true)
        {
            if (Player.IsWall)
                break;

            if (Vector3.Distance(originPos, transform.position) >= rushDistance)
            {
                break;
            }

            transform.Translate(rushDir * Time.deltaTime * RushSpeed, Space.World);
            yield return null;
        }
    }

    private void Move()
    {
        if (Player.MoveDir == Vector3.zero)
            return;
        Player.LookAtMoveDir();

        // �÷��̾� �̵�
        // ���� �ְ� ���� �ε����� ���� ���¿����� �̵�
        if (Player.IsGround == false && Player.IsWall == true)
            return;
        Vector3 originRb = Rb.velocity;
        Vector3 velocityDir = transform.forward * (Model.MoveSpeed * _moveSpeedMultyPlier);
        Rb.velocity = new Vector3(velocityDir.x, originRb.y, velocityDir.z);
    }

    private void StopCoroutines()
    {
        if (_chargeRoutine != null)
        {
            CoroutineHandler.StopRoutine(_chargeRoutine);
            _chargeRoutine = null;
        }
        if (_autoAttackRoutine != null)
        {
            CoroutineHandler.StopRoutine(_autoAttackRoutine);
            _autoAttackRoutine = null;
        }
    }
    private void ProcessAutoAttackTmer()
    {
        _isAutoAttack = true;
        _autoAttackTime += Time.deltaTime;
        if (_autoAttackTime > _autoAttackDelay)
        {
            ChargeEnd();
        }
    }
}
