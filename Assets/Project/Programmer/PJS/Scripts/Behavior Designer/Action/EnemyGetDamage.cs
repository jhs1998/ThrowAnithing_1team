using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Assets.Project.Programmer.NSJ.RND.Script;

public class EnemyGetDamage : Action
{
	// 충돌한 오브젝트, 해당 오브젝트의 공격력
	// 내 자신의 hp
	public SharedGameObject triggerObj;

	private BaseEnemy enemy;
	private float playerDamage;

	public override void OnStart()
	{
		enemy = GetComponent<BaseEnemy>();

		// TODO : 플레이어 투척 오브젝트 스크립트 확인 후 변경
		//damage = triggerObj.Value.GetComponent<TestCodeData>().Atk;
	}

	public override TaskStatus OnUpdate()
	{
		return enemy.GetDamage(playerDamage) ? TaskStatus.Success : TaskStatus.Failure;
	}
}