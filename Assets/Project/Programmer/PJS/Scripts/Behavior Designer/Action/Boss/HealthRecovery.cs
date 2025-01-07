using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using Unity.Collections;
using UnityEngine;

public class HealthRecovery : Action
{
    public int addHp;
    public float maxTime = 15f;   // 최대 시간()
    public float minTime = 3f;   // 최소 시간
    public int maxAddHp = 10;    // 회복 되는 최대 수치
    [ReadOnly] public int minAddHp;        // 회복하는 최소 수치

    const float persent = 100f;
    private BossEnemy enemy;
    public float minRecoveryPersent;  // 최소 회복할 퍼센트 ex) 초당 n% 회복
    public float maxRecoveryPersent;  // 최대 회복할 퍼센트 ex) n%까지 회복

    public override void OnStart()
    {
        enemy = GetComponent<BossEnemy>();

        minAddHp = maxAddHp / (int)(maxTime / minTime);
        minRecoveryPersent = minAddHp / persent;
        maxRecoveryPersent = maxAddHp / persent;
    }

    public override TaskStatus OnUpdate()
    {
        StartCoroutine(RecoveryRoutin());

        return TaskStatus.Success;
    }

    IEnumerator RecoveryRoutin()
    {
        //while ()    // 다 회복 되었는가?
        //{
        //    enemy.CurHp += (int)(enemy.GetState().MaxHp * 0.2f);
        //    yield return minTime.GetDelay();
        //}

        yield return minTime.GetDelay();
        enemy.CurHp += (int)(enemy.GetState().MaxHp * minRecoveryPersent);
        Debug.Log("시간지남");
    }
}