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
        EnemyBullet bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation).GetComponent< EnemyBullet>();
        bullet.target = playerObj.Value.transform;
        bullet.Speed = bulletSpeed;
        bullet.Atk = state.Atk;
        bullet.transform.parent = transform;
        bullet.Battle = Battle;
    }
}
