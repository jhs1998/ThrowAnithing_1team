using BehaviorDesigner.Runtime;
using System;
using UnityEngine;

[System.Serializable]
public class State
{
    public string Name;
    [Range(50, 3000)] public int MaxHp;  // 체력
    [Range(0, 20)] public int Atk;       // 공격력
    [Range(0, 10)] public float Def;    // 방어력
    [Range(0, 10)] public float Speed;    // 이동 속도
    [Range(0, 10)] public float AtkDelay;   // 공격 속도
    [Range(0, 10)] public float AttackDis;  // 공격 사거리
    [Range(0, 10)] public float TraceDis;   // 인식 사거리
}

[RequireComponent(typeof(BattleSystem))]
public class BaseEnemy : MonoBehaviour, IHit,IDebuff
{
    [SerializeField] protected BehaviorTree tree;

    [Header("몬스터 기본 스테이터스")]
    [SerializeField] protected State state;
    [Header("아이템 드랍 확률(100 단위)")]
    [SerializeField] float reward;
    [Header("현재 체력")]
    [SerializeField] int curHp;

    [HideInInspector] int maxHp;      // 최대 체력
    [HideInInspector] float speed;      // 이동속도
    [HideInInspector] float jumpPower;  // 점프력
    [HideInInspector] float attackSpeed;// 공격속도

    [HideInInspector] public int resultDamage;  // 최종적으로 피해 입는 데미지
    [HideInInspector] public Collider[] overLapCollider = new Collider[100];
    [HideInInspector] public BattleSystem Battle;

    public int Damage { get { return state.Atk; } }
    public int MaxHp {  get { return maxHp; } set { maxHp = value; } }
    public int CurHp { get { return curHp; } set { curHp = value; } }
    public float MoveSpeed { get { return speed; } set { speed = value; } }
    public float JumpPower { get { return jumpPower; } set { jumpPower = value; } }
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }



    protected SharedGameObject playerObj;

    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        tree = GetComponent<BehaviorTree>();
        Battle = GetComponent<BattleSystem>();
        // FIXME : 나중에 수정 필요
        gameObject.layer = Layer.Monster;
    }

    private void Start()
    {
        SettingVariable();
        curHp = state.MaxHp;
        speed = state.Speed;
        attackSpeed = state.AtkDelay;
    }

    public State GetState()
    {
        return state;
    }

    private void SettingVariable()
    {
        tree.SetVariable("PlayerObj", playerObj);
        tree.SetVariable("PlayerTrans", (SharedTransform)playerObj.Value.transform);
        tree.SetVariable("Speed", (SharedFloat)state.Speed);
        tree.SetVariable("AtkDelay", (SharedFloat)state.AtkDelay);
        tree.SetVariable("TraceDis", (SharedFloat)state.TraceDis);
        tree.SetVariable("AttackDis", (SharedFloat)state.AttackDis);
        tree.SetVariable("Reward", (SharedFloat)reward);
    }

    /// <summary>
    /// 몬스터가 피해받는 데미지
    /// </summary>
    public void TakeDamage(int damage, bool isStun)
    {
        resultDamage = damage - (int)state.Def;
        tree.SetVariableValue("TakeDamage", true);

        if (resultDamage <= 0)
            resultDamage = 0;

        curHp -= resultDamage;

        // TODO : 결과 값은 TakeDamage 매개변수로 변환
        Debug.Log($"TakeDamage : {isStun}") ;
        tree.SetVariableValue("Stiff", isStun);
        Debug.Log($"{resultDamage} 피해를 입음. curHP : {curHp}");
    }

    /// <summary>
    /// 차지 후 폭발 데미지 부여
    /// </summary>
    public void TakeChargeBoom(float range, int damage)
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, range, overLapCollider);
        for (int i = 0; i < hitCount; i++)
        {
            IHit hit = overLapCollider[i].GetComponent<IHit>();
            if (hit != null)
            {
                if (overLapCollider[i].gameObject.name.CompareTo("Boss") == 0)
                    return;

                hit.TakeDamage(damage, false);
            }
        }
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
