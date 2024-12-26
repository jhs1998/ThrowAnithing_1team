using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStalactite : MonoBehaviour
{
    [SerializeField] GameObject ray;
    RaycastHit hit;
    [SerializeField] float maxDistance = 15f;

    [SerializeField] GameObject stalactite;
    [SerializeField] Transform stalactiteBeforeMove;
    [SerializeField] Transform stalactiteAfterMove;
    //레이를 쏘고있다가
    //플레이어가 들어오면 가시 내림
    //가시는 플레이어랑 부딪히면 체력 -40깎게함
    //가시는 5초뒤면 사라짐


    private void Update()
    {
        Debug.DrawRay(ray.transform.position, Vector3.down, Color.red);
        Debug.DrawLine(ray.transform.position, Vector3.down, Color.red);
        if (Physics.Raycast(ray.transform.position, Vector3.down, out hit, maxDistance))
        {
            if (hit.collider.gameObject.tag == Tag.Player)
            {
                //TODO: 천천히 떨어지게 고치기
                stalactite.transform.position = 
                    Vector3.MoveTowards(stalactiteBeforeMove.position, stalactiteAfterMove.position, 5f);
            }
            
        }


    }


}
