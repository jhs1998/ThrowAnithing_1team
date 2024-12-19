using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    [SerializeField] EnemyBullet bulletPrefab;
    [SerializeField] Transform muzzle;

    public void Attack()
    {
        Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
    }
}
