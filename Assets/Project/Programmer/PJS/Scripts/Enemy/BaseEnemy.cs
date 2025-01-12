using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class State
{
    public string Name;
    [Range(50, 10000)] public int MaxHp;  // 체력
    [Range(0, 100)] public int Atk;       // 공격력
    [Range(0, 100)] public float Def;    // 방어력
    [Range(0, 100)] public float Speed;    // 이동 속도
    [Range(0, 100)] public float AtkDelay;   // 공격 속도
    [Range(0, 100)] public float AttackDis;  // 공격 사거리
    [Range(0, 100)] public float TraceDis;   // 인식 사거리
}

[RequireComponent(typeof(BattleSystem))]
public class BaseEnemy : MonoBehaviour, IHit, IDebuff
{
    [SerializeField] protected BehaviorTree tree;

    [Header("몬스터 기본 스테이터스")]
    [SerializeField] protected State state;
    [Header("아이템 드랍 확률(100 단위)"), Range(0, 100)]
    [SerializeField] float reward;
    [Header("현재 체력")]
    [SerializeField] int curHp;
    [Header("파티클's")]
    [SerializeField] ParticleSystem stepMoveParticle;
    [SerializeField] ParticleSystem dieParticle;

    [HideInInspector] float jumpPower;  // 점프력

    [HideInInspector] public int resultDamage;  // 최종적으로 피해 입는 데미지
    [HideInInspector] public Collider[] overLapCollider = new Collider[100];
    [HideInInspector] public BattleSystem Battle;

    public int Damage { get { return state.Atk; } }
    public int MaxHp { get { return state.MaxHp; } set { state.MaxHp = value; } }
    public int CurHp { get { return curHp; } set { curHp = value; } }
    public float MoveSpeed { get { return state.Speed; } set { state.Speed = value; } }
    public float JumpPower { get { return jumpPower; } set { jumpPower = value; } }
    public float AttackSpeed { get { return state.AtkDelay; } set { state.AtkDelay = value; } }

    protected SharedGameObject playerObj;

    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        tree = GetComponent<BehaviorTree>();
        Battle = GetComponent<BattleSystem>();
    }

    private void Start()
    {
        BaseInit();
    }

    // 이동 애니메이션 이벤트
    public void BeginStepMove()
    {
        stepMoveParticle.Play();
    }

    // 사망 애니메이션 이벤트
    public void DeadMotion()
    {
        dieParticle.Play();
    }

    public State GetState()
    {
        return state;
    }

    public BehaviorTree GetBT()
    {
        return tree;
    }

    protected void BaseInit()
    {
        SettingVariable();
        curHp = state.MaxHp;
    }

    private void SettingVariable()
    {
        tree.SetVariable("PlayerObj", playerObj);
        tree.SetVariable("PlayerTrans", (SharedTransform)playerObj.Value.transform);
        tree.SetVariable("Speed", (SharedFloat)MoveSpeed);
        tree.SetVariable("AtkDelay", (SharedFloat)state.AtkDelay);
        tree.SetVariable("TraceDis", (SharedFloat)state.TraceDis);
        tree.SetVariable("AttackDis", (SharedFloat)state.AttackDis);
        tree.SetVariable("Reward", (SharedFloat)reward);
    }

    public int TakeDamage(int damage, bool isIgnoreDef)
    {
        resultDamage = isIgnoreDef == true ? damage : damage - (int)state.Def;
        tree.SetVariableValue("TakeDamage", true);

        if (resultDamage <= 0)
            resultDamage = 0;

        curHp -= resultDamage;

        
        Debug.Log($"{resultDamage} 피해를 입음. curHP : {curHp}");
        return resultDamage;
    }

    public void TakeCrowdControl(CrowdControlType type)
    {
        tree.SetVariableValue("Stiff", type == CrowdControlType.Stiff);
    }
    /// <summary>
    /// 반경 내 폭발 데미지 부여
    /// </summary>
    public void TakeChargeBoom(float range, int damage)
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, range, overLapCollider);
        for (int i = 0; i < hitCount; i++)
        {
            IBattle hit = overLapCollider[i].GetComponent<IBattle>();
            if (hit != null)
            {
                if (overLapCollider[i].gameObject.name.CompareTo("Boss") == 0)
                    continue;

                hit.TakeDamage(damage, true);
            }
        }
    }

    /// <summary>
    /// 패턴 쿨타임 관련 코루틴
    /// </summary>
    /// <param name="atkAble">해당 스킬의 bool 타입</param>
    /// <param name="coolTime">쿨타임 시간</param>
    public IEnumerator CoolTimeRoutine(SharedBool atkAble, float coolTime)
    {
        atkAble.SetValue(false);
        yield return coolTime.GetDelay();
        atkAble.SetValue(true);
    }

    private void OnDrawGizmosSelected()
    {
        // 거리 그리기
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, state.TraceDis);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, state.AttackDis);
    }
}
