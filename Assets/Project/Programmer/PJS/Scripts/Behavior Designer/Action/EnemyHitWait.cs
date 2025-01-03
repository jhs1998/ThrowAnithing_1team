using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyHitWait : Action
{
    [SerializeField] Animator anim;

    public override void OnStart()
    {
        anim = GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}