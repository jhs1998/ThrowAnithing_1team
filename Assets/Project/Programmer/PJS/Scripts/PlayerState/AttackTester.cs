using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTester : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float angle;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    public void Attack()
    {
        // 전방 앞에 있는 몬스터들을 확인하고 데미지를 준다.
        // 1. 범위안에 몬스터를 확인한다.
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach(Collider collider in colliders)
        {
            // 2. 각도 내에 있는지 확인한다.
            Vector3 source = transform.position;
            source.y = 0;
            Vector3 destination = collider.transform.position;
            destination.y = 0;


            Vector3 targetDir = (destination - source).normalized;
            float targetAngle = Vector3.Angle(transform.forward, targetDir);
            if (targetAngle > angle * 0.5f)
                continue;

            Debug.Log(collider.gameObject.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 거리 그리기
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        // 각도 그리기
        Vector3 rightDir = Quaternion.Euler(0, angle * 0.5f, 0) * transform.forward;
        Vector3 leftDir = Quaternion.Euler(0, angle * -0.5f, 0) * transform.forward;

        Gizmos.color = Color.blue;

    }
}
