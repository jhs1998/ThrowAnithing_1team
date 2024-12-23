using Assets.Project.Programmer.NSJ.RND.Script;
using BehaviorDesigner.Runtime;
using System;
using UnityEngine;

[System.Serializable]
public class State
{
    [Range(50, 1000)] public int MaxHp;  // 체력
    [Range(0, 20)] public int Atk;       // 공격력
    [Range(0, 10)] public float Def;    // 방어력
    [Range(0, 10)] public float Speed;    // 이동 속도
    [Range(0, 10)] public float AtkDelay;   // 공격 속도
    [Range(0, 10)] public float AttackDis;  // 공격 사거리
    [Range(0, 10)] public float TraceDis;   // 인식 사거리
}

public class BaseEnemy : MonoBehaviour, IHit
{
    [SerializeField] BehaviorTree tree;

    [Header("몬스터 기본 스테이터스")]
    [SerializeField] protected State state;
    [Header("아이템 드랍 확률(100 단위)")]
    [SerializeField] float reward;
    [Header("현재 체력")]
    [SerializeField] int curHp;
    public int Damage { get { return state.Atk; } }
    public int CurHp { get { return curHp; } }

    protected SharedGameObject playerObj;

    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        tree = GetComponent<BehaviorTree>();
    }

    private void Start()
    {
        SettingVariable();
        curHp = state.MaxHp;
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
    public bool GetDamage(float damage)
    {
        float finalDamage = damage - state.Def;

        if (finalDamage <= 0)
            return false;

        curHp -= (int)finalDamage;
        Debug.Log($"{(int)damage} 피해를 입음. curHP : {curHp}");
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        // 거리 그리기
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, state.TraceDis);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, state.AttackDis);
    }

    public void TakeDamage(int damage)
    {
        int finalDamage = damage - (int)state.Def;

        if (finalDamage <= 0)
            finalDamage = 0;

        curHp -= finalDamage;
        Debug.Log($"{damage} 피해를 입음. curHP : {curHp}");
    }
}
