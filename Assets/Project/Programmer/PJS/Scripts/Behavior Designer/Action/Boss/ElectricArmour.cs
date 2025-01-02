using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ElectricArmour : Action
{
	[SerializeField] BossSkillState skillState;
	[SerializeField] GlobalState globalState;

    private BossEnemy enemy;

	public override void OnStart()
	{
		enemy = GetComponent<BossEnemy>();
    }

    public override TaskStatus OnUpdate()
	{
        StartCoroutine(enemy.CoolTimeRoutine(skillState.atkAble, skillState.coolTime));
		StartCoroutine(enemy.CoolTimeRoutine(globalState.Able, globalState.coolTime.Value));
		return TaskStatus.Success;
	}
}