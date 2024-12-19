using BehaviorDesigner.Runtime;
using System;
using UnityEngine;

[System.Serializable]
public class State
{
    [Range(100, 1000)] public int MaxHp;  // 체력
    [Range(0, 50)] public int Atk;       // 공격력
    [Range(0, 10)] public float Def;    // 방어력
    [Range(0, 10)] public float Speed;    // 이동 속도
    [Range(0, 50)] public float TraceDis;  // 인식 사거리
    [Range(0, 10)] public float AttackDis; // 공격 사거리
}


public class BaseEnemy : MonoBehaviour
{
    [SerializeField] BehaviorTree tree;

    [SerializeField] protected State state;
    [SerializeField] int curHp;
    public int Damge { get { return state.Atk; } }
    public int Hp { get { return curHp; } }

    private SharedGameObject playerObj;

    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        tree = GetComponent<BehaviorTree>();
    }

    private void Start()
    {
        tree.SetVariable("PlayerObj", playerObj);
        tree.SetVariable("PlayerTrans", (SharedTransform)playerObj.Value.transform);
        tree.SetVariable("TraceDis", (SharedFloat)state.TraceDis);
        tree.SetVariable("AttackDis", (SharedFloat)state.AttackDis);
        tree.SetVariable("Speed", (SharedFloat)state.Speed);

        curHp = state.MaxHp;
    }

    public void GetDamage(float damage)
    {
        float finalHp = curHp + state.Def;

        if(finalHp < damage)
        {
            curHp = 0;
            return;
        }

        curHp = (int)(finalHp - damage);
        Debug.Log($"{(int)damage} 피해를 입음. curHP : {curHp}");
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
