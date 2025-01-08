using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanJump : Conditional
{
	public SharedGameObject player;
	public float dis;

	public override TaskStatus OnUpdate()
	{
		dis = (transform.position - player.Value.transform.position).sqrMagnitude;

		return TaskStatus.Success;
	}
}