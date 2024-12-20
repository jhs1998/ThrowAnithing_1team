using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : Action
{
    [SerializeField] Animator anim;
    [SerializeField] SharedFloat reward;
    [SerializeField] List<GameObject> dropItem;


    public override void OnAwake()
    {
        anim = GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie Death") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            // 몬스터가 죽었을 시 아이템 드랍
            int randNum = Random.Range(0, 101);
            Debug.Log(randNum);

            // TODO : 확률 인스펙터에서 정해서 값 가져오기
            if (randNum <= reward.Value)
            {
                Debug.Log("재화 생성");
            }

            return TaskStatus.Success;
        }

        anim.SetBool("Deadth", true);
        return TaskStatus.Running;
    }
}