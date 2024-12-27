using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class LightningPist : Action
{
    [SerializeField] BossSkillState skillState;

    private BossEnemy enemy;

    public override void OnStart()
    {
        enemy = GetComponent<BossEnemy>();
    }

    public override TaskStatus OnUpdate()
    {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, transform.lossyScale / 2f, transform.forward, transform.rotation, skillState.range);

        for (int i = 0; i < hits.Length; i++)
        {
            IHit hitObj = hits[i].collider.GetComponent<IHit>();
            if (hitObj != null)
            {
                Debug.Log(hits[i].collider.gameObject);
                if (hits[i].collider.gameObject.name.CompareTo("Boss") == 0)
                    continue;

                hitObj.TakeDamage(skillState.damage, true);
            }
        }

        StartCoroutine(enemy.CoolTimeRoutine(skillState.atkAble, skillState.coolTime));

        return TaskStatus.Success;
    }
}