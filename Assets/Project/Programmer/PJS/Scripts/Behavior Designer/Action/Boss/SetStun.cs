using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SetStun : Action
{
	public SharedBool able;
	public bool onStun;
	private BossEnemy enemy;
	 
	public override void OnStart()
	{
		enemy = GetComponent<BossEnemy>();
		able.SetValue(true);
	}

	public override TaskStatus OnUpdate()
	{
		enemy.breakShield = onStun;
		return TaskStatus.Success;
	}
}