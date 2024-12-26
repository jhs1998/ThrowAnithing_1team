using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using BehaviorDesigner.Runtime;

public class LightningNova : Action
{
	[SerializeField] int atkDamage;	// 공격력
	[SerializeField] float range;   // 피해 입히는 범위
	[SerializeField] float coolTime;	// 쿨타임
	[SerializeField] SharedBool atkAble;	// 공격 사용 여부
	
	private BaseEnemy enemy;

	public override void OnStart()
	{
		enemy = GetComponent<BaseEnemy>();
	}

	public override TaskStatus OnUpdate()
	{
		if(atkAble.Value == false)
			return TaskStatus.Failure;

		enemy.TakeChargeBoom(range, atkDamage);

		StartCoroutine(CoolTimeRoutine());

		return TaskStatus.Success;
	}

	IEnumerator CoolTimeRoutine()
	{
		atkAble.SetValue(false);
		Debug.Log("쿨타임 시작");
		yield return coolTime.GetDelay();
		atkAble.SetValue(true);
		Debug.Log("쿨타임 끝");
	}
}