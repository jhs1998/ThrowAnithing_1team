using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using UnityEngine;

public class HealthRecovery : Action
{
    public SharedInt maxTime = 15;   // 최대 시간 n초
    public SharedInt maxAddHp = 10;    // 회복 되는 최대 수치 n%

    private BossEnemy enemy;
    private float minRecoveryPersent;  // 최소 회복하는 퍼센트 ex) 초당 n%
    public SharedBool able;  // 사용 여부 확인

    public override void OnStart()
    {
        enemy = GetComponent<BossEnemy>();
        enemy.createShield = true;
        minRecoveryPersent = (maxAddHp.Value / 100f) / maxTime.Value;
    }

    public override TaskStatus OnUpdate()
    {
        if (enemy.recovery != null && enemy.createShield == true)
        {
            return TaskStatus.Running;
        }
        else if (enemy.createShield == false)
        {
            // 회복이 끝남 -> 일반 상태로 전환 => 코루틴에서 해결함
            // 실드가 깨짐 -> 그로기 상태로 전환
            able.SetValue(true);
            return TaskStatus.Failure;
        }
        
        enemy.RecoveryStartCoroutine(maxTime.Value, minRecoveryPersent);
        return TaskStatus.Success;
    }
}