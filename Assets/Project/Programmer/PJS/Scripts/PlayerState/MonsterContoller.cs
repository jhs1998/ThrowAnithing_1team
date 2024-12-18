using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterContoller : MonoBehaviour
{
    public void TakeHit(int damage)
    {
        Debug.Log($"몬스터가 {damage} 데미지 피격 받았다.");
    }
}
