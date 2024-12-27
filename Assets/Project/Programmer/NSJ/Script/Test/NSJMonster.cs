using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NSJMonster : MonoBehaviour, IHit
{
    [SerializeField] private int _hp;
    [SerializeField] private int _damage = 1;
    private Renderer _renderer;
    private Color _origin;
    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _origin = _renderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Tag.Player)
        {
            IHit hitable = collision.gameObject.GetComponent<IHit>();
            hitable.TakeDamage(_damage, false);
        }
    }

    public void TakeDamage(int damage, bool isStun)
    {
        _hp -= damage;
        Debug.Log($"{name} 데미지를 입음. 데미지 {damage} , 남은체력 {_hp}");

        StartCoroutine(HitRoutine());
    }

    [SerializeField] private List<HitAdditional> _debuffList = new List<HitAdditional>();
    public void AddDebuff(HitAdditional debuff)
    {
        int index = _debuffList.FindIndex(origin => origin.Origin.Equals(debuff.Origin));
        if (index >= _debuffList.Count)
            return;

        HitAdditional cloneDebuff = Instantiate(debuff);
        // 디버프 중복 시
        if (index != -1)
        {
            // 기존 디버프 삭제
            _debuffList[index].Exit();
            Destroy(_debuffList[index]);
            _debuffList.RemoveAt(index);
        }
        // 디버프 추가 후 발동
        _debuffList.Add(cloneDebuff);
        cloneDebuff.Target = gameObject;
        cloneDebuff.OnExitHitAdditional += RemoveDebuff;
        cloneDebuff.Enter();
    }
    private void RemoveDebuff(HitAdditional debuff)
    {
        _debuffList.Remove(debuff);
    }
    IEnumerator HitRoutine()
    {

        _renderer.material.color = Color.yellow;

        yield return 0.2f.GetDelay();

        _renderer.material.color = _origin;
    }

}
