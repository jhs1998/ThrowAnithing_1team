using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    [Header("피격 모션 쿨타임")]
    [SerializeField] float hitCoolTime;
    [Header("투사체 속도")]
    [SerializeField] float bulletSpeed;
    [Header("투사체 정보")]
    [SerializeField] EnemyBullet bulletPrefab;
    [SerializeField] Transform muzzle;

    public float BulletSpeed { get { return bulletSpeed; } }


    private void Start()
    {
        BaseInit();
        tree.SetVariableValue("HitCoolTime", hitCoolTime);
    }

    public void Attack()
    {
        EnemyBullet bulletPool = ObjectPool.GetPool(bulletPrefab, muzzle.position, muzzle.rotation);
        bulletPool.target = playerObj.Value.transform;
        bulletPool.Speed = bulletSpeed;
        bulletPool.Atk = state.Atk;
        bulletPool.Battle = Battle;
    }
}
