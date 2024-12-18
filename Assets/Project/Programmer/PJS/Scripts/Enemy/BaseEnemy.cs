using BehaviorDesigner.Runtime;
using System;
using UnityEngine;

[System.Serializable]
public class State
{
    public int Hp;  // 체력
    [Range(0, 50)]public int Damge;       // 공격 데미지
    [Range(0, 50)]public float TraceDis;  // 인식 사거리
    [Range(0, 10)]public float AttackDis; // 공격 사거리
}


public class BaseEnemy : MonoBehaviour
{
    [SerializeField] BehaviorTree tree;

    [SerializeField] protected State state; 

    public int Damge { get { return state.Damge; } }
    public int Hp { get { return state.Hp; } }

    private SharedGameObject playerObj;
    private SharedTransform playerTrans;

    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        tree = GetComponent<BehaviorTree>();
    }

    private void Start()
    {
        tree.SetVariable("PlayerObj", playerObj);
        tree.SetVariable("PlayerTrans", playerTrans);
        tree.SetVariable("TraceDis", (SharedFloat)state.TraceDis);
        tree.SetVariable("AttackDis", (SharedFloat)state.AttackDis);
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
