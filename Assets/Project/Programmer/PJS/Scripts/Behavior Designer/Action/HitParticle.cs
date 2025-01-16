using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HitParticle : BaseAction
{
	public ParticleSystem hitParticle;
    public SharedBool takeDamege;
    private ParticleSystem hit;

    public override void OnEnd()
    {
        ObjectPool.ReturnPool(hit);
        takeDamege.SetValue(false);
    }

    public override TaskStatus OnUpdate()
	{
        hit = ObjectPool.GetPool(hitParticle, baseEnemy.Battle.HitPoint);
        hit.Play();

        return TaskStatus.Success;
	}
}