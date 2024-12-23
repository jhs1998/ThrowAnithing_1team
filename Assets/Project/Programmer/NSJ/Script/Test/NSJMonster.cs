using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSJMonster : MonoBehaviour, IHit
{
    [SerializeField] private int _hp;
    [SerializeField] private List<HitAdditional> _debuffList = new List<HitAdditional>();
    private Renderer _renderer;
    private Color _origin;
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _origin = _renderer.material.color;
    }
    public void TakeDamage(int damage)
    {
        _hp -= damage;
       // Debug.Log($"{name} 데미지를 입음. 데미지 {damage} , 남은체력 {_hp}");

        StartCoroutine(HitRoutine());
    }

    public void AddDebuff(HitAdditional debuff)
    {
        int index = _debuffList.FindIndex(origin => origin.Origin.Equals(debuff.Origin));
        if (index >= _debuffList.Count)
            return;

        // 디버프 중복 시
        if (index != -1)
        {
            // 기존 디버프 삭제
            _debuffList[index].Exit();
            Destroy(_debuffList[index]);
            _debuffList.RemoveAt(index);
        }
        // 디버프 추가 후 발동
        _debuffList.Add(debuff);
        debuff.Target = gameObject;
        debuff.OnExitHitAdditional += RemoveDebuff;
        debuff.Enter();
    }
    IEnumerator HitRoutine()
    {

        _renderer.material.color = Color.yellow;

        yield return 0.2f.GetDelay();

        _renderer.material.color = _origin;
    }

    private void RemoveDebuff(HitAdditional debuff)
    {
        _debuffList.Remove(debuff);
    }
}
