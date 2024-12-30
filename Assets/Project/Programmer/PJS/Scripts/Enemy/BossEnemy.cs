using BehaviorDesigner.Runtime;
using System.Collections;
using UnityEngine;

public class BossEnemy : BaseEnemy
{
    public enum Phase { Phase1, Phase2, Phase3 }
    public Phase curPhase = Phase.Phase1;
    //[SerializeField] BossEnemyState bossState;
    [SerializeField] ParticleSystem shieldParticle;

    private Coroutine attackAble;
    private bool onFrezenyPassive = false;

    private void FrenzyPassive()
    {
        if (onFrezenyPassive == false)
            return;

        Debug.Log($"{MoveSpeed}, {AttackSpeed}");

        MoveSpeed = MoveSpeed + (MoveSpeed * 0.2f);
        AttackSpeed = AttackSpeed - (AttackSpeed * 0.2f);

        Debug.Log($"{MoveSpeed}, {AttackSpeed}");
    }

    IEnumerator PassiveOn()
    {
        while (true)
        {
            if (curPhase == Phase.Phase3)
            {
                FrenzyPassive();
                yield break;
            }

            yield return null;
        }
    }

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
        Debug.Log("ThunderStomp()");
        // 체력의 의한 패턴 변경
        if (CurHp > state.MaxHp * 0.8f)
        {
            // 1페이즈 - 일렉트릭 아머
            //Debug.Log("curHp > 80");
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
        //Debug.Log("Shooting()");
        //AttackMelee();
        // 일반 근접 공격 - 모든 페이즈에 존재
        // 라이트닝 피스트 - 1페이즈에만 존재
    }

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
            Battle.TargetAttackWithDebuff(overLapCollider[i], finalDamage, true);
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
        Debug.Log($"{atkAble.Name} 쿨타임 시작");
        yield return coolTime.GetDelay();
        atkAble.SetValue(true);
        Debug.Log($"{atkAble.Name} 쿨타임 끝");
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

        // 거리 그리기
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, state.TraceDis);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, state.AttackDis);
    }
}
