using BehaviorDesigner.Runtime;
using System.Collections;
using UnityEngine;

public class BossEnemy : BaseEnemy
{
    [SerializeField] BossEnemyState bossState;
    [SerializeField] public ParticleSystem particle;
    [SerializeField] ParticleSystem shieldParticle;
    [HideInInspector] public Coroutine attackAble;

    private void Update()
    {
        if(tree.GetVariable("LightningPist") == (SharedBool)true)
            particle.Play();
    }

    /// <summary>
    /// Move 애니메이션 이벤트
    /// </summary>
    public void FootStep()
    {
        Debug.Log("FootSteop()");
    }

    /// <summary>
    /// Attack 애니메이션 이벤트
    /// </summary>
    public void OnHitBegin()
    {
        Debug.Log("OnHitBegin()");
    }
    public void OnHitEnd()
    {
        Debug.Log("OnHitEnd()");
    }

    /// <summary>
    /// Attack_3 애니메이션 이벤트
    /// </summary>
    public void ThunderStomp()
    {
        Debug.Log("ThunderStomp()");
        if (CurHp >= state.MaxHp * 0.8)
            Debug.Log("80퍼 이상");

        shieldParticle.Play();
        // 체력의 의한 패턴 변경
        // 1페이즈 - 일렉트릭 아머
        // 2페이즈 - 레이지 스톰
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
        Debug.Log("Shooting()");
        // 일반 근접 공격 - 모든 페이즈에 존재
        // 라이트닝 피스트 - 1페이즈에만 존재
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
    /// <returns></returns>
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

    /// <summary>
    /// 패턴 쿨타임 관련 코루틴
    /// </summary>
    /// <param name="atkAble">해당 스킬의 bool 타입</param>
    /// <param name="coolTime">쿨타임 시간</param>
    public IEnumerator CoolTimeRoutine(SharedBool atkAble, float coolTime)
    {
        atkAble.SetValue(false);
        Debug.Log("쿨타임 시작");
        yield return coolTime.GetDelay();
        atkAble.SetValue(true);
        Debug.Log("쿨타임 끝");
    }

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
    }
}
