using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashBoom", menuName = "AdditionalEffect/Player/DashBoom")]
public class DashBoom : PlayerAdditional
{
    [System.Serializable]
    struct EffectStrcut
    {
        public GameObject EffectPrefab;
        [HideInInspector] public GameObject Effect;
    }
    [SerializeField] EffectStrcut _effect;

    [Header("���� ����")]
    [SerializeField] float _range;
    [Header("���� �ð�")]
    [SerializeField] float _stunTime;
    public override void Trigger()
    {
        if(Player.CurState == PlayerController.State.Dash)
        {
            Attack();
        }
    }

    private void Attack()
    {
        GameObject effect= ObjectPool.GetPool(_effect.EffectPrefab, transform.position, Quaternion.identity, 1f);
        effect.transform.localScale *= _range;

        int hitCount = Physics.OverlapSphereNonAlloc(Battle.HitPoint.position, _range, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            // TODO : ���� ���� ��� 
            Battle.TargetCrowdControl(Player.OverLapColliders[i], CrowdControlType.Stun, _stunTime);
        }
    }


    //public override void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, _range);
    //}
}
