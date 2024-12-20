using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyAttack : Action
{
    [SerializeField] Animator anim;

    public override void OnAwake()
    {
        anim = GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
    {
        // TODO : 공격 애니메이션 확인 후 퍼센트 정하기
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie Attack") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            anim.SetBool("Attack", false);
            return TaskStatus.Success;
        }

        anim.SetBool("Attack", true);
        return TaskStatus.Running;
    }
}