using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class DeadEnemy : Action
{
	[SerializeField] Animator anim;

    public override void OnAwake()
    {
        anim = GetComponent<Animator>();
    }

	public override TaskStatus OnUpdate()
	{
		anim.Play("Zombie Deadth");

		return TaskStatus.Success;
	}
}