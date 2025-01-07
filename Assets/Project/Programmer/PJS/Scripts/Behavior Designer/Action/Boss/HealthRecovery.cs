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

    public override void OnStart()
    {
        enemy = GetComponent<BossEnemy>();

        minRecoveryPersent = (maxAddHp.Value / 100f) / maxTime.Value;
    }

    public override TaskStatus OnUpdate()
    {
        StartCoroutine(RecoveryRoutin());

        return TaskStatus.Success;
    }

    IEnumerator RecoveryRoutin()
    {
        int time = maxTime.Value;
        int recoveryHp = Mathf.RoundToInt(enemy.GetState().MaxHp * minRecoveryPersent);
        Debug.Log(time);
        while (time > 0)    // 회복 하는 시간
        {
            yield return 1f.GetDelay();
            enemy.CurHp += recoveryHp;
            time--;
            Debug.Log("회복");
        }
    }
}