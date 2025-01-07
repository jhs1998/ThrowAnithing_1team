using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

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

    [Space, SerializeField] ParticleSystem shieldParticle;

    private Coroutine attackAble;
    public Coroutine recovery;  // 회복 관련 코루틴
    private bool onFrezenyPassive = false;
    private bool onEntryStop;
    [HideInInspector]public bool createShield;
    [HideInInspector]public bool breakShield;

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
    }

    IEnumerator RecoveryRoutin(int maxTime, float recoveryValue)
    {
        int time = maxTime;
        int recoveryHp = Mathf.RoundToInt(state.MaxHp * recoveryValue);
        Debug.Log("회복 시작");
        while (time > 0)    // 회복 하는 시간
        {
            yield return 1f.GetDelay();
            CurHp += recoveryHp;
            time--;
            Debug.Log("회복 중...");
        }

        Debug.Log("회복 끝");
        // 회복 끝
        createShield = false;
        transform.GetComponent<Animator>().SetBool("Recovery", false);
    }

    /// <summary>
    /// 몬스터가 피해받는 데미지
    /// </summary>
    public new int TakeDamage(int damage, bool isStun)
    {
        if(createShield == true)
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

        tree.SetVariableValue("Stiff", isStun);
        Debug.Log($"{resultDamage} 피해를 입음. curHP : {CurHp}");
        return resultDamage;
    }


    #region 애니메이션 이벤트
    /// <summary>
    /// Move 애니메이션 이벤트
    /// </summary>
    public void FootStep()
    {
        //Debug.Log("FootSteop()");
    }

    /// <summary>
    /// Attack 애니메이션 이벤트
    /// </summary>
    public void OnHitBegin()
    {
        //Debug.Log("OnHitBegin()");
    }
    public void OnHitEnd()
    {
        //Debug.Log("OnHitEnd()");
    }

    /// <summary>
    /// Attack_3 애니메이션 이벤트
    /// </summary>
    public void ThunderStomp()
    {
        // 체력의 의한 패턴 변경
        if (CurHp > state.MaxHp * 0.8f)
        {
            // 1페이즈 - 일렉트릭 아머
            shieldParticle.Play();
        }
        else if (CurHp <= state.MaxHp * 0.8f && CurHp > state.MaxHp * 0.5f)
        {
            // 2페이즈 - 레이지 스톰
            Debug.Log("80 >= curHP > 50");
        }

    }

    /// <summary>
    /// 사망 애니메이션 이벤트
    /// </summary>
    public void DieEff()
    {
        Debug.Log("DieEff()");
    }
    public void ShakingForAni()
    {
        Debug.Log("ShakingForAni()");
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
        Debug.Log("공격 딜레이 시작");
        yield return state.AtkDelay.GetDelay();
        // 공격 딜레이 끝
        tree.SetVariableValue("AttackAble", true);
        Debug.Log("공격 딜레이 끝");
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
