using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using UnityEngine;

public class HealthRecovery : BossAction
{
    public SharedInt maxTime = 15;   // �ִ� �ð� n��
    public SharedInt maxAddHp = 10;    // ȸ�� �Ǵ� �ִ� ��ġ n%

    private float minRecoveryPersent;  // �ּ� ȸ���ϴ� �ۼ�Ʈ ex) �ʴ� n%
    public SharedBool able;  // ��� ���� Ȯ��

    public override void OnStart()
    {
        bossEnemy.createShield = true;
        minRecoveryPersent = (maxAddHp.Value / 100f) / maxTime.Value;
    }

    public override TaskStatus OnUpdate()
    {
        if (bossEnemy.recovery != null && bossEnemy.createShield == true)
        {
            return TaskStatus.Running;
        }
        else if (bossEnemy.createShield == false)
        {
            // ȸ���� ���� -> �Ϲ� ���·� ��ȯ => �ڷ�ƾ���� �ذ���
            // �ǵ尡 ���� -> �׷α� ���·� ��ȯ
            able.SetValue(true);
            return TaskStatus.Failure;
        }

        bossEnemy.RecoveryStartCoroutine(maxTime.Value, minRecoveryPersent);
        return TaskStatus.Success;
    }
}