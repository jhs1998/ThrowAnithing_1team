using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    [Header("투사체 속도")]
    [SerializeField] float bulletSpeed;
    [Header("투사체 정보")]
    [SerializeField] EnemyBullet bulletPrefab;
    [SerializeField] Transform muzzle;

    public float BulletSpeed { get { return bulletSpeed; } }

    public void Attack()
    {
        EnemyBullet bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation).GetComponent< EnemyBullet>();
        bullet.Speed = bulletSpeed;
        bullet.Atk = state.Atk;
        bullet.transform.parent = transform;
        bullet.Battle = Battle;
    }
}
