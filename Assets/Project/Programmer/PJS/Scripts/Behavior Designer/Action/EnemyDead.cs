using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyDead : Action
{
	[SerializeField] Animator anim;

    public override void OnAwake()
    {
        anim = GetComponent<Animator>();
    }

	public override TaskStatus OnUpdate()
	{
		anim.SetBool("Deadth", true);

		return TaskStatus.Success;
	}
}