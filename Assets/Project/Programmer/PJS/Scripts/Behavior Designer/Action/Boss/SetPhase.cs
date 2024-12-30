using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using static BossEnemy;

public class SetPhase : Action
{
    [SerializeField] SharedString phase;

    private BossEnemy enemy;

    public override void OnStart()
    {
        enemy = GetComponent<BossEnemy>();
    }

    public override TaskStatus OnUpdate()
    {
        // 현재 체력으로 페이지 변경
        if (enemy.CurHp > enemy.MaxHp * 0.8f)
        {
            //Debug.Log("curHp > 80");
            enemy.curPhase = Phase.Phase1;
        }
        else if (enemy.CurHp <= enemy.MaxHp * 0.8f && enemy.CurHp > enemy.MaxHp * 0.5f)
        {
            //Debug.Log("80 >= curHP > 50");
            enemy.curPhase = Phase.Phase2;
        }
        else if (enemy.CurHp <= enemy.MaxHp * 0.5f && enemy.CurHp > enemy.MaxHp * 0.3f)
        {
            //Debug.Log("50 >= curHp > 30");
            enemy.curPhase = Phase.Phase3;
        }

        return TaskStatus.Failure;
    }
}