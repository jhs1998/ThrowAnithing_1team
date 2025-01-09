using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject.SpaceFighter;

public class BossEnemy : BaseEnemy, IHit
{
    public enum PhaseType { Phase1, Phase2, Phase3 }
    public PhaseType curPhase = PhaseType.Phase1;

    [Header("회복할 최대 시간 ( 초 단위)")]
    [SerializeField] int maxTime;
    [Header("회복할 최대 HP ( % 단위)"), Range(0, 100)]
    [SerializeField] int maxRecoveryHp;
    [Header("2페이즈에 생성되는 실드 파괴 카운트")]
    [SerializeField] int breakshieldCount = 20;
    [Header("실드 파괴 후 그로기 시간 ( 초 단위)")]
    [SerializeField] float stunTime;

    [Space, SerializeField] ParticleSystem shieldParticle;
    public float jumpForce;
    private Coroutine attackAble;
    public Coroutine recovery;  // 회복 관련 코루틴
    private bool onFrezenyPassive = false;
    private bool onEntryStop;
    [HideInInspector] public bool createShield;
    [HideInInspector] public bool breakShield;

    private void Start()
    {
        BaseInit();
        StateInit();
        StartCoroutine(PassiveOn());

        this.UpdateAsObservable()
            .Select(x => CurHp)
            .Subscribe(x => ChangePhase());
    }

    private void StateInit()
    {
        tree.SetVariableValue("MaxTime", maxTime);
        tree.SetVariableValue("MaxRecoveryHp", maxRecoveryHp);
        tree.SetVariableValue("StunTime", stunTime);
    }

    private void ChangePhase()
    {
        // 현재 체력으로 페이즈 변경
        if (CurHp > MaxHp * 0.8f && onEntryStop == false)
        {
            curPhase = PhaseType.Phase1;
            onEntryStop = true;
        }
        else if (CurHp <= MaxHp * 0.8f && CurHp > MaxHp * 0.5f)
        {
            curPhase = PhaseType.Phase2;
        }
        else if (CurHp <= MaxHp * 0.5f && CurHp > MaxHp * 0.3f)
        {
            curPhase = PhaseType.Phase3;
            //onFrezenyPassive = true;
        }
    }

    private void FrenzyPassive()
    {
        if (onFrezenyPassive == false)
        {
            return;
        }

        tree.SetVariableValue("Speed", MoveSpeed + (MoveSpeed * 0.2f));
        tree.SetVariableValue("AtkDelay", AttackSpeed - (AttackSpeed * 0.2f));

        onFrezenyPassive = false;
    }

    IEnumerator PassiveOn()
    {
        while (true)
        {
            if (curPhase == PhaseType.Phase3)
            {
                tree.SetVariableValue("Speed", MoveSpeed + (MoveSpeed * 0.2f));
                tree.SetVariableValue("AtkDelay", AttackSpeed - (AttackSpeed * 0.2f));
                yield break;
            }

            yield return null;
        }
    }

    // 회복관련 루틴
    public void RecoveryStartCoroutine(int time, float value)
    {
        recovery = StartCoroutine(RecoveryRoutin(time, value));
    }
    public void RecoveryStopCotoutine()
    {
        StopCoroutine(recovery);
        transform.GetComponent<Animator>().SetBool("Recovery", false);
    }

    IEnumerator RecoveryRoutin(int maxTime, float recoveryValue)
    {
        int time = maxTime;
        int recoveryHp = Mathf.RoundToInt(state.MaxHp * recoveryValue);

        while (time > 0)    // 회복 하는 시간
        {
            yield return 1f.GetDelay();
            CurHp += recoveryHp;
            time--;
        }

        // 회복 끝
        createShield = false;
        transform.GetComponent<Animator>().SetBool("Recovery", false);
    }

    /// <summary>
    /// 몬스터가 피해받는 데미지
    /// </summary>
    public new int TakeDamage(int damage, bool isIgnoreDef)
    {
        if (createShield == true)
        {
            breakshieldCount--;
            if (breakshieldCount <= 0) // 실드 깨짐
            {
                breakShield = true;
                createShield = false;
            }

            return 0;
        }

        resultDamage = damage - (int)state.Def;
        tree.SetVariableValue("TakeDamage", true);

        if (resultDamage <= 0)
            resultDamage = 0;

        CurHp -= resultDamage;

        return resultDamage;
    }


    #region 애니메이션 이벤트
    /// <summary>
    /// Move 애니메이션 이벤트
    /// </summary>
    public void FootStep()
    {
    }

    /// <summary>
    /// Attack 애니메이션 이벤트
    /// </summary>
    public void OnHitBegin()
    {
    }
    public void OnHitEnd()
    {
    }

    /// <summary>
    /// Attack_3 애니메이션 이벤트
    /// </summary>
    public void ThunderStomp()
    {
        shieldParticle.Play();
    }

    /// <summary>
    /// 사망 애니메이션 이벤트
    /// </summary>
    public void DieEff()
    {
    }
    public void ShakingForAni()
    {
    }

    /// <summary>
    /// 일반 공격 애니메이션 이벤트
    /// </summary>
    public void Shooting()
    {
        if (transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Zombie Attack"))
            AttackMelee();
        // 일반 근접 공격 - 모든 페이즈에 존재
        // 라이트닝 피스트 - 1페이즈에만 존재
    }

    public void JumpAttackBegin()
    {
        Vector3 playerPos = new Vector3(playerObj.Value.transform.position.x, 
                                        transform.position.y, 
                                        playerObj.Value.transform.position.z);

        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        //transform.position = Vector3.Lerp(transform.position, playerObj.Value.transform.position, 0.01f);
        /*Vector3 jumpMove = Vector3.MoveTowards(transform.position, playerObj.Value.transform.position, state.Speed * Time.deltaTime);
        Debug.Log(jumpMove);
        GetComponent<Rigidbody>().MovePosition(jumpMove);*/
    }
    public void JumpAttackEnd()
    {
        TakeChargeBoom(2, 30);
    }
    #endregion

    /// <summary>
    /// 근접 공격
    /// </summary>
    public void AttackMelee()
    {
        // 전방 앞에 있는 몬스터들을 확인하고 피격 진행
        // 1. 전방에 있는 몬스터 확인
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 attackPos = playerPos;
        int hitCount = Physics.OverlapSphereNonAlloc(attackPos, state.AttackDis, overLapCollider, 1 << Layer.Player);
        for (int i = 0; i < hitCount; i++)
        {
            // 2. 각도 내에 있는지 확인
            Vector3 source = transform.position;
            source.y = 0;
            Vector3 destination = overLapCollider[i].transform.position;
            destination.y = 0;
            Vector3 targetDir = (destination - source).normalized;
            float targetAngle = Vector3.Angle(transform.forward, targetDir); // 아크코사인 필요 (느리다)

            if (targetAngle > 120 * 0.5f)
                continue;

            int finalDamage = state.Atk;
            // 데미지 주기
            //Battle.TargetAttackWithDebuff(overLapCollider[i], finalDamage, true);
            Battle.TargetAttack(overLapCollider[i], finalDamage, true);
        }
    }

    /// <summary>
    /// 공격 가능 여부 확인
    /// </summary>
    public void AttackAble()
    {
        if (attackAble != null)
            return;

        attackAble = StartCoroutine(AttackDelayRoutine());
    }

    /// <summary>
    /// 공격 딜레이
    /// </summary>
    IEnumerator AttackDelayRoutine()
    {
        // 공격 딜레이 시작
        tree.SetVariableValue("AttackAble", false);
        yield return state.AtkDelay.GetDelay();
        // 공격 딜레이 끝
        tree.SetVariableValue("AttackAble", true);
    }

    #region Gizmo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (true == Physics.BoxCast(transform.position, transform.lossyScale / 2.0f, transform.forward, out RaycastHit hit, transform.rotation, 8))
        {
            // Hit된 지점까지 ray를 그려준다.
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);

            // Hit된 지점에 박스를 그려준다.
            Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, transform.lossyScale);
        }
        else
        {
            // Hit가 되지 않았으면 최대 검출 거리로 ray를 그려준다.
            Gizmos.DrawRay(transform.position, transform.forward * 8);
        }

        // 거리 그리기
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, state.TraceDis);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, state.AttackDis);
    }
    #endregion
}
