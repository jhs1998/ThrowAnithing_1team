using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanStiff : Conditional
{
	[SerializeField] SharedBool Stiff;

    public override void OnEnd()
    {
        Stiff.Value = false;
    }

    public override TaskStatus OnUpdate()
	{
		return Stiff.Value ? TaskStatus.Success : TaskStatus.Failure;
	}
}