using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TestStop : Action
{
	[SerializeField] SharedFloat speed;

	private float preSpeed;

	public override void OnStart()
	{
		preSpeed = speed.Value;
	}

    public override void OnEnd()
    {
        speed.SetValue(preSpeed);
    }

    public override TaskStatus OnUpdate()
	{
		speed.Value = 0;
		return TaskStatus.Success;
	}
}