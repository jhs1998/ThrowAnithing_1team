using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    [Header("���� ȿ����")]
    [SerializeField] List<AudioClip> attackClips;
    [Header("�ǰ� ��� ��Ÿ��")]
    [SerializeField] float hitCoolTime;

    private void Start()
    {
        BaseInit();
        tree.SetVariableValue("HitCoolTime", hitCoolTime);
    }

    // ���� �ִϸ��̼� �̺�Ʈ
    public void BeginAttack()
    {
        AttackMelee();
        SoundManager.PlaySFX(ChoiceAudioClip(attackClips));
    }
    public void EndAttack()
    {
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void AttackMelee()
    {
        // ���� �տ� �ִ� ���͵��� Ȯ���ϰ� �ǰ� ����
        // 1. ���濡 �ִ� ���� Ȯ��
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 attackPos = playerPos;
        int hitCount = Physics.OverlapSphereNonAlloc(attackPos, state.AttackDis, overLapCollider, 1 << Layer.Player);
        for (int i = 0; i < hitCount; i++)
        {
            // 2. ���� ���� �ִ��� Ȯ��
            Vector3 source = transform.position;
            source.y = 0;
            Vector3 destination = overLapCollider[i].transform.position;
            destination.y = 0;

            Vector3 targetDir = (destination - source).normalized;
            float targetAngle = Vector3.Angle(transform.forward, targetDir); // ��ũ�ڻ��� �ʿ� (������)
            if (targetAngle > 120 * 0.5f)
                continue;

            int finalDamage = state.Atk;
            // ������ �ֱ�
            Battle.TargetAttackWithDebuff(overLapCollider[i], finalDamage);
            Battle.TargetCrowdControl(overLapCollider[i], CrowdControlType.Stiff);
        }
    }
}
