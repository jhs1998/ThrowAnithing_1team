using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyBulletInstance : Action
{
    [SerializeField] SharedGameObject bulletPrefab;
    [SerializeField] SharedTransform muzzlePos;

    [SharedRequired]
    public SharedGameObject result;

    public override TaskStatus OnUpdate()
    {
        GameObject.Instantiate(bulletPrefab.Value, muzzlePos.Value.position, muzzlePos.Value.rotation);
        return TaskStatus.Success;
    }
}