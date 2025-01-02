using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TakeHit : Action
{
	public SharedBool hitAble;
	public float coolTime;

	private BaseEnemy enemy;

    public override void OnStart()
    {
        enemy = GetComponent<BaseEnemy>();
    }

    public override TaskStatus OnUpdate()
	{
        StartCoroutine(enemy.CoolTimeRoutine(hitAble, coolTime));

        return TaskStatus.Success;
	}
}