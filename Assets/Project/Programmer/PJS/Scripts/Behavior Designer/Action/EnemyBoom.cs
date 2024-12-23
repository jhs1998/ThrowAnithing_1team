using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Assets.Project.Programmer.NSJ.RND.Script;

public class EnemyBoom : Action
{
	[SerializeField] SharedBool isBoom;
    [SerializeField] SharedFloat attackDist;

    private BaseEnemy enemy;

	public override void OnStart()
	{
		enemy = GetComponent<BaseEnemy>();
	}

	public override TaskStatus OnUpdate()
	{
        if (isBoom.Value == true) 
            return TaskStatus.Failure;

        Collider[] colliders = Physics.OverlapSphere(transform.position, attackDist.Value);
        foreach (Collider collider in colliders)
        {
            IHit hit = collider.transform.GetComponent<IHit>();
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