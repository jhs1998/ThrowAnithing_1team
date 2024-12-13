using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSJMonster : MonoBehaviour, IHit
{
    [SerializeField] private int _hp;
    private Renderer _renderer;
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();        
    }
    public void TakeDamage(int damage)
    {
       _hp -= damage;
        Debug.Log($"{name} 데미지를 입음. 데미지 {damage} , 남은체력 {_hp}");

        StartCoroutine(HitRoutine());
    }

    IEnumerator HitRoutine()
    {
        Color origin = _renderer.material.color;
        _renderer.material.color = Color.yellow;

        yield return 0.2f.GetDelay();

        _renderer.material.color = origin;
    }
}
