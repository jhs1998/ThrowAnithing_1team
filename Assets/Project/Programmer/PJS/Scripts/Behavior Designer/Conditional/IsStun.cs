using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsStun : Conditional
{
	private BossEnemy enemy;

    public override void OnStart()
    {
        enemy = GetComponent<BossEnemy>();
    }

    public override void OnEnd()
    {
        enemy.breakShield = false;
    }

    public override TaskStatus OnUpdate()
	{
        if(enemy.breakShield == true)
        {
            Debug.Log(123);
            enemy.RecoveryStopCotoutine();
        }

        return enemy.breakShield ? TaskStatus.Success : TaskStatus.Failure;
	}
}