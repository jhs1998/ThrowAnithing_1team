using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Zenject.SpaceFighter;

public class LookAtPlayer : Action
{
	public SharedTransform player;

	public override TaskStatus OnUpdate()
	{
        Vector3 movePos = new(
            player.Value.position.x,
        transform.position.y,
            player.Value.position.z);

		transform.LookAt(movePos);

		return TaskStatus.Success;
	}
}