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
        anim.SetBool("Deadth", true);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie Death") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            gameObject.SetActive(false);
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}