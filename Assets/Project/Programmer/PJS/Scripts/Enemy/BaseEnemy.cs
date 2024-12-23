using Assets.Project.Programmer.NSJ.RND.Script;
using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using TMPro;
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

    [HideInInspector] public int resultDamage;
    public int Damage { get { return state.Atk; } }
    public int CurHp { get { return curHp; } set { curHp = value; } }

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
        tree.SetVariable("CurHp", (SharedInt)curHp);
    }

    private void OnDrawGizmosSelected()
    {
        // 거리 그리기
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, state.TraceDis);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, state.AttackDis);
    }

    /// <summary>
    /// 몬스터가 피해받는 데미지
    /// </summary>
    public void TakeDamage(int damage)
    {
        resultDamage = damage - (int)state.Def;

        if (resultDamage <= 0)
            resultDamage = 0;

        curHp -= resultDamage;
        Debug.Log($"{damage} 피해를 입음. curHP : {curHp}");
    }
}
