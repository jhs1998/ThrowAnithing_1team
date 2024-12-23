using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyTrace : Action
{
    [SerializeField] SharedFloat speed;         // 몬스터 이동속도
    [SerializeField] SharedTransform player;    // 플레이어
    [SerializeField] SharedFloat traceDist;          // 인식 거리
    [SerializeField] SharedFloat attackDis;

    public override TaskStatus OnUpdate()
    {
        float dir = (player.Value.position - transform.position).magnitude;
        
        if (dir < attackDis.Value)
            return TaskStatus.Success;
        else if(dir > traceDist.Value)
            return TaskStatus.Failure;

        transform.position = Vector3.MoveTowards(transform.position, player.Value.position, speed.Value * Time.deltaTime);
        transform.LookAt(player.Value);
        return TaskStatus.Running;
    }
}
