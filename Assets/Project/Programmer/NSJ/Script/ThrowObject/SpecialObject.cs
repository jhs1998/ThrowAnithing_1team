using System.Collections;
using System.Collections.Generic;
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
    struct EffectStruct
    {
        public GameObject EffectPrefab;
    }
    // ���� ����Ʈ �ʵ�
    [SerializeField] private EffectStruct _effect;

    private List<Transform> MiddleHittargets = new List<Transform>();
    protected override void OnCollisionEnter(Collision collision)
    {
        // ���� ��Ƶ� ����
        ObjectPool.ReturnPool(gameObject);
    }

    protected override void OnDisable()
    {
        ProcessExplosion();
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
    protected override void HitTarget()
    {
        if (CanAttack == false)
            return;
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
        CoroutineHandler.StartRoutine(CreateExplosionEffect());
    }
    /// <summary>
    /// ����Ʈ ��ƾ
    /// </summary>
    IEnumerator CreateExplosionEffect()
    {
        GameObject effect = ObjectPool.GetPool(_effect.EffectPrefab, transform.position, transform.rotation);
        yield return 1f.GetDelay();
        ObjectPool.ReturnPool(effect);
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