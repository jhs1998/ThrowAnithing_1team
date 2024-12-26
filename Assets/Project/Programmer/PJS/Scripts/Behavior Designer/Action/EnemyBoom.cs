using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Assets.Project.Programmer.NSJ.RND.Script;

public class EnemyBoom : Action
{
	[SerializeField] SharedBool isBoom;
    [SerializeField] SharedFloat attackDist;    // Æø¹ß ¹üÀ§

    private BaseEnemy enemy;

	public override void OnStart()
	{
		enemy = GetComponent<BaseEnemy>();
	}

	public override TaskStatus OnUpdate()
	{
        if (isBoom.Value == true) 
            return TaskStatus.Failure;

        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, attackDist.Value, enemy.overLapCollider);
        for (int i = 0; i < hitCount; i++)
        {
            IHit hit = enemy.overLapCollider[i].GetComponent<IHit>();
            if (hit != null)
            {
                hit.TakeDamage(enemy.Damage);
            }
        }

        if(enemy.CurHp > 0)
            enemy.CurHp = -1;

        isBoom.SetValue(true);
        return TaskStatus.Success;
	}
}