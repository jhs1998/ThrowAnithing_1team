using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class LightningPist : Action
{
	[SerializeField] float range;

	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		RaycastHit[] hits = Physics.BoxCastAll(transform.forward, transform.position, transform.forward, transform.rotation, range);

		for (int i = 0;	i< hits.Length;i++)
		{
			Debug.Log(hits[i].collider.gameObject);
		}

		return TaskStatus.Success;
	}
}