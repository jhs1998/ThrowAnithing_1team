using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HitParticle : Action
{
	public ParticleSystem hitParticle;
    public SharedBool takeDamege;
	private BaseEnemy enemy;
    private ParticleSystem hit;

    public override void OnAwake()
    {
        enemy = GetComponent<BaseEnemy>();
    }

    public override void OnEnd()
    {
        ObjectPool.ReturnPool(hit);
        takeDamege.SetValue(false);
    }

    public override TaskStatus OnUpdate()
	{
        hit = ObjectPool.GetPool(hitParticle, enemy.Battle.HitPoint);
        hit.Play();

        return TaskStatus.Success;
	}
}