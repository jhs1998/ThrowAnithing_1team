using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GetPlayerPos : Action
{
	public SharedGameObject player;
	public SharedVector3 playerpos;

	public override void OnStart()
	{
        Vector3 playerPos = new Vector3(player.Value.transform.position.x, transform.position.y, player.Value.transform.position.z);
        //player.Value.transform.position
        playerpos.SetValue(playerPos);
	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}