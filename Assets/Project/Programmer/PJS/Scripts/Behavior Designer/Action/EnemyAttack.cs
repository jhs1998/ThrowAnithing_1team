using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyAttack : Action
{
	[SerializeField] Animator anim;

    public override void OnAwake()
    {
        anim = GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
    {
        //anim.SetTrigger("Attack");
        anim.SetBool("Attack 0", true);

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie Attack") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            anim.SetBool("Attack 0", false);
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}