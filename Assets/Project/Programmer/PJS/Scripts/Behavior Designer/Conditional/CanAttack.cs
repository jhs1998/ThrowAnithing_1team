using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class CanAttack : Conditional
{
    [SerializeField] SharedTransform playerPos;
    [SerializeField] SharedFloat attackDis;
    [SerializeField] SharedBool attakAble;

    private float distance;

    public override TaskStatus OnUpdate()
    {
        distance = (playerPos.Value.position - transform.position).magnitude;

        return distance <= attackDis.Value ? TaskStatus.Success : TaskStatus.Failure;
    }
}
