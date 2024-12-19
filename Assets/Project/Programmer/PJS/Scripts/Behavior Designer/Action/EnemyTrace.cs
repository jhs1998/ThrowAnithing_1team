using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyTrace : Action
{
    [SerializeField] SharedFloat speed;         // 몬스터 이동속도
    [SerializeField] SharedTransform player;    // 플레이어
    [SerializeField] SharedFloat dist;

    public override TaskStatus OnUpdate()
    {
        float dir = (player.Value.position - transform.position).magnitude;
        
        if (dir < 1f)
            return TaskStatus.Success;
        else if(dir > dist.Value)
            return TaskStatus.Failure;

        transform.position = Vector3.MoveTowards(transform.position, player.Value.position, speed.Value * Time.deltaTime);
        transform.LookAt(player.Value);
        return TaskStatus.Running;
    }
}
