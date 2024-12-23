using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanBoom : Conditional
{
	[SerializeField] SharedBool isBoom;

	public override TaskStatus OnUpdate()
	{
		return isBoom.Value ? TaskStatus.Success : TaskStatus.Failure;
	}
}