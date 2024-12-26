using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyDead : Action
{
    [SerializeField] Animator anim;

    public override void OnAwake()
    {
        anim = GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie Death") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Collider collider = GetComponent<Collider>();
            collider.isTrigger = true;

            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}