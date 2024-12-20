using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyHitWait : Action
{
	[SerializeField] Animator anim;
	[SerializeField] SharedFloat speed;

	float speedValue;

	public override void OnStart()
	{
		anim = GetComponent<Animator>();
		speedValue = speed.Value;
	}

	public override TaskStatus OnUpdate()
	{
		if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
		{
			speed.Value = speedValue;
			return TaskStatus.Success;
		}

		speed.Value = 0;
		return TaskStatus.Running;
	}
}