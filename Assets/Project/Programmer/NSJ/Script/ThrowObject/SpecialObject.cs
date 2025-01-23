using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialObject : ThrowObject
{
    struct ExplosionStruct
    {
        public float Damage;
        public float MiddleDamage;
        public float Range;
        public float MiddleRange;
    }
    // ���� ������, ���� �ʵ�
    private ExplosionStruct _explosion;
    [System.Serializable]
    new struct EffectStruct
    {
        public GameObject EffectPrefab;
        public GameObject ThrowTailEffectPrefab;
        [HideInInspector]public GameObject ThrowTailEffect;
    }
    // ����Ʈ �ʵ�
    [SerializeField] private EffectStruct _effect;

    private List<Transform> MiddleHittargets = new List<Transform>();
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        // ����Ʈ ����
        _effect.ThrowTailEffect = ObjectPool.GetPool(_effect.ThrowTailEffectPrefab,transform);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        // ���� ��Ƶ� ����
        ProcessExplosion();
        ObjectPool.ReturnPool(gameObject);
    }

    protected override void OnDisable()
    {


        base.OnDisable();
    }
    /// <summary>
    /// ���� �ʵ� ����
    /// </summary>
    public void InitSpecial(float damage, float middleDamage, float range, float middleRange)
    {
        _explosion.Damage = damage;
        _explosion.MiddleDamage = middleDamage;
        _explosion.Range = range;
        _explosion.MiddleRange = middleRange;   
    }


    /// <summary>
    /// Ÿ�� ����
    /// </summary>
    protected override void HitTarget(Collider other)
    {
        if (CanAttack == false)
            return;
        // ���� ����Ʈ ����

        ProcessExplosion();
        _effect.ThrowTailEffect.transform.SetParent(null);
        ObjectPool.ReturnPool(_effect.ThrowTailEffect, 0.5f);
        ObjectPool.ReturnPool(gameObject);
    }

    /// <summary>
    /// ����
    /// </summary>
    private void ProcessExplosion() 
    {
        // �߾� ���� ���� �ɷ�����
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _explosion.MiddleRange, Player.OverLapColliders, 1 << Layer.Monster);
        for(int i = 0; i < hitCount; i++)
        {
            MiddleHittargets.Add(Player.OverLapColliders[i].transform);
        }


        hitCount = Physics.OverlapSphereNonAlloc(transform.position, _explosion.Range, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++) 
        {
            int finalDamage = 0;
            bool isCritical = false;
            // �߾ӿ� �¾��� ���
            if (MiddleHittargets.Contains(Player.OverLapColliders[i].transform))
            {
                finalDamage = Player.GetFinalDamage((int)_explosion.MiddleDamage, out isCritical);
            }
            else
            {
                finalDamage = Player.GetFinalDamage((int)_explosion.Damage, out isCritical);
            }
         
            Battle.TargetAttack(Player.OverLapColliders[i], isCritical, finalDamage);
            Battle.TargetCrowdControl(Player.OverLapColliders[i], CrowdControlType.Stiff);
        }

        // ����Ʈ
        ObjectPool.GetPool(_effect.EffectPrefab, transform.position, transform.rotation, 1f);

        // ����
        SoundManager.PlaySFX(Player.Sound.Balance.Special2Hit);
    }

    #region Ư��ȿ��
    // Ư�������� Ư��ȿ�� ������ �ȵ�
    public override void EnterThrowAdditional() { }   
    public override void ExitThrowAdditional() { }       
    public override void UpdateThrowAdditional() { }           
    public override void FixedUpdateThrowAdditional() { }
    public override void TriggerThrowAddtional() { }
    #endregion
}
