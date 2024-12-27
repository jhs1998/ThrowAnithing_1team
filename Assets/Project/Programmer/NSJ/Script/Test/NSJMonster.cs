using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NSJMonster : MonoBehaviour, IHit, IDebuff
{
    [HideInInspector] public BattleSystem Battle;
    [SerializeField] private int _maxHp;
    [SerializeField] private int _hp;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _damage = 1;
    private Renderer _renderer;
    private Color _origin;
    public int MaxHp { get { return _maxHp; } set {  _maxHp = value; } }
    public int CurHp { get { return _hp; } set { _hp = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float JumpPower { get; set; }
    public float AttackSpeed { get; set; }

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        Battle = GetComponent<BattleSystem>();
        _origin = _renderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Tag.Player)
        {
            Battle.TargetAttack(collision.transform, _damage, true);
        }
    }

    public void TakeDamage(int damage, bool isStun)
    {
        _hp -= damage;
       // Debug.Log($"{name} 데미지를 입음. 데미지 {damage} , 남은체력 {_hp}");

        StartCoroutine(HitRoutine());
    }
    IEnumerator HitRoutine()
    {

        _renderer.material.color = Color.yellow;

        yield return 0.2f.GetDelay();

        _renderer.material.color = _origin;
    }

}
