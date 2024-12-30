using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject.SpaceFighter;

public class MeleeEnemy : BaseEnemy
{
    [SerializeField] CapsuleCollider attakArm;

    //private bool _canAttack;
    public void BeginAttack()
    {
        AttackMelee();
    }

    public void EndAttack()
    {
     
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.tag == Tag.Player && _canAttack == true)
    //    {
    //        IHit hit = other.transform.GetComponent<IHit>();
    //        hit.TakeDamage(Damage, true);
    //    }
    //}

    /// <summary>
    /// 근접 공격
    /// </summary>
    public void AttackMelee()
    {
        // 전방 앞에 있는 몬스터들을 확인하고 피격 진행
        // 1. 전방에 있는 몬스터 확인
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 attackPos = playerPos;
        int hitCount = Physics.OverlapSphereNonAlloc(attackPos, state.AttackDis, overLapCollider, 1 << Layer.Player);
        for (int i = 0; i < hitCount; i++)
        {
            // 2. 각도 내에 있는지 확인
            Vector3 source = transform.position;
            source.y = 0;
            Vector3 destination = overLapCollider[i].transform.position;
            destination.y = 0;

            Vector3 targetDir = (destination - source).normalized;
            float targetAngle = Vector3.Angle(transform.forward, targetDir); // 아크코사인 필요 (느리다)
            if (targetAngle > 120 * 0.5f)
                continue;


            int finalDamage = state.Atk;
            // 데미지 주기
            Battle.TargetAttackWithDebuff(overLapCollider[i], finalDamage, true);
        }
    }
}
