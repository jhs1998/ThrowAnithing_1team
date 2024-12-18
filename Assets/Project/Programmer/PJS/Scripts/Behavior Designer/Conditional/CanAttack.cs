using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class CanAttack : Conditional
{
    [SerializeField] SharedTransform playerPos;
    [SerializeField] SharedFloat attackDis;

    private float distance;

    public override TaskStatus OnUpdate()
    {
        distance = (playerPos.Value.position - transform.position).magnitude;

        if(distance < attackDis.Value)
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }
}
