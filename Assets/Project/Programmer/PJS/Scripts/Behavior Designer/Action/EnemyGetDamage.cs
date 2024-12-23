using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyGetDamage : Action
{
	// 충돌한 오브젝트, 해당 오브젝트의 공격력
	// 내 자신의 hp
	[SerializeField] EnemyDamage damageText;
	[SerializeField] Transform textPos;
	private BaseEnemy enemy;

	// TODO : 피격 당했을 시 데미지 UI로 보여주기
	public override void OnStart()
	{
		enemy = GetComponent<BaseEnemy>();
	}

	public override TaskStatus OnUpdate()
	{
		EnemyDamage eDmg = GameObject.Instantiate(damageText, textPos);
		eDmg.transform.SetParent(textPos);
		eDmg.Damage = enemy.resultDamage;

		return TaskStatus.Success;
	}
}