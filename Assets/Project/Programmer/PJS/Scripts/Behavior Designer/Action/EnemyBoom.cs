using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

public class EnemyBoom : Action
{
	[SerializeField] SharedBool isBoom;
    [SerializeField] SharedFloat attackDist;    // Æø¹ß ¹üÀ§
    [SerializeField] ParticleSystem paticle;
    public List<AudioClip> deathClips = new List<AudioClip>();
    private BaseEnemy enemy;

    public override void OnAwake()
    {
		enemy = GetComponent<BaseEnemy>();

        foreach (AudioClip clip in enemy.GetDaethClips())
        {
            deathClips.Add(clip);
        }
    }

	public override TaskStatus OnUpdate()
	{
        // Æø¹ß ¿©ºÎ - true => ÀÌ¹Ì ÀÚÆøÇÔ, false => ÀÚÆøÇÏÁö ¾ÊÀ½
        if (isBoom.Value == true) 
            return TaskStatus.Failure;

        // Æø¹ß
        enemy.TakeChargeBoom(attackDist.Value, enemy.Damage);

        if(enemy.CurHp > 0)
            enemy.CurHp = -1;

        SoundManager.PlaySFX(deathClips[Random.Range(0, deathClips.Count)]);
        paticle.Play();
        isBoom.SetValue(true);

        return TaskStatus.Success;
	}
}