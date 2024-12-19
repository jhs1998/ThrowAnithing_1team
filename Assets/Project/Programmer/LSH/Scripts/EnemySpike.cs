using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpike : MonoBehaviour
{
    // To Do : n초마다 지속적으로 장애물에게 공격받기 플레이어 스크립트로 옮기기
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //other.gameObject.

        }

    }
}
