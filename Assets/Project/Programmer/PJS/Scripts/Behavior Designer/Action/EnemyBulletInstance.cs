using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyBulletInstance : Action
{
    [SerializeField] SharedGameObject bulletPrefab;
    [SerializeField] SharedTransform muzzlePos;

    private RangedEnemy enemy;

    public override void OnStart()
    {
        enemy = GetComponent<RangedEnemy>();
    }

    public override TaskStatus OnUpdate()
    {
        EnemyBullet bullet = GameObject.Instantiate(bulletPrefab.Value, muzzlePos.Value.position, muzzlePos.Value.rotation).GetComponent<EnemyBullet>();
        bullet.Speed = enemy.BulletSpeed;
        bullet.transform.parent = transform;

        return TaskStatus.Success;
    }
}