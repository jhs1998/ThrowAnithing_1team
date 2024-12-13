using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSJMonster : MonoBehaviour, IHit
{
    [SerializeField] private int _hp;
    public void TakeDamage(int damage)
    {
       _hp -= damage;
        Debug.Log($"{name} 데미지를 입음. 데미지 {damage} , 남은체력 {_hp}");
    }
}
