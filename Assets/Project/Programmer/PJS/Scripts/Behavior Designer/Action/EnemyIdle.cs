using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyIdle : Action
{
    [SerializeField] Animator anim;

    public override void OnStart()
    {
        anim = GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie Idle") == false &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            anim.Play("Zombie Idle");

        return TaskStatus.Running;
    }
}