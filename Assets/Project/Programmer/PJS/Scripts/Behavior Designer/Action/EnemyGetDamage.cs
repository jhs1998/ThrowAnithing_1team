using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyGetDamage : Action
{
	[SerializeField] EnemyDamageText damageText;	// 데미지 Text
	[SerializeField] Transform textPos;		// 생성 위치

	private BaseEnemy enemy;

	// TODO : 피격 당했을 시 데미지 UI로 보여주기
	public override void OnStart()
	{
		enemy = GetComponent<BaseEnemy>();
	}

	public override TaskStatus OnUpdate()
	{
		EnemyDamageText eDmg = GameObject.Instantiate(damageText, textPos);
		eDmg.transform.SetParent(textPos);	// UI 생성은 transform.parent보다 SetParent로 사용해야 오류가 사라짐
		eDmg.Damage = enemy.resultDamage;

		return TaskStatus.Success;
	}
}