using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyGetDamage : Action
{
	// 충돌한 오브젝트, 해당 오브젝트의 공격력
	// 내 자신의 hp
	public SharedGameObject triggerObj;

	private BaseEnemy enemy;
	private float damage;

	public override void OnStart()
	{
		enemy = GetComponent<BaseEnemy>();

		// TODO : 오브젝트 스크립트 확인 후 변경
		damage = triggerObj.Value.GetComponent<TestCodeData>().Atk;
	}

	public override TaskStatus OnUpdate()
	{
		enemy.GetDamage(damage);
		return TaskStatus.Success;
	}
}