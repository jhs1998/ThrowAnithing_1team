using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using BehaviorDesigner.Runtime;

public class LightningNova : Action
{
	[SerializeField] int atkDamage;	// 공격력
	[SerializeField] float range;   // 피해 입히는 범위
	[SerializeField] float coolTime;
	[SerializeField] SharedBool atkAble;
	
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
		yield return new WaitForSeconds(coolTime);
		atkAble.SetValue(true);
		Debug.Log("쿨타임 끝");
	}
}