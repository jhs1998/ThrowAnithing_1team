using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using BehaviorDesigner.Runtime;

public class LightningNova : Action
{
    [SerializeField] BossSkillState skillState;

    private BossEnemy enemy;

	public override void OnStart()
	{
		enemy = GetComponent<BossEnemy>();
	}

    public override TaskStatus OnUpdate()
	{
		enemy.TakeChargeBoom(skillState.range, skillState.damage);

		StartCoroutine(enemy.CoolTimeRoutine(skillState.atkAble, skillState.coolTime));

		return TaskStatus.Success;
	}
}