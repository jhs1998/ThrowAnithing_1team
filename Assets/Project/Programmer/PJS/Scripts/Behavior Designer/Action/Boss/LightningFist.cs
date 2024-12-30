using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class LightningFist : Action
{
    [SerializeField] BossSkillState skillState;
    [SerializeField] GlobalState globalState;
    [SerializeField] GameObject electricZone;
    [SerializeField] Transform createPos;

    private BossEnemy enemy;

    public override void OnStart()
    {
        enemy = GetComponent<BossEnemy>();
    }

    public override TaskStatus OnUpdate()
    {
        RaycastHit[] hits = Physics.BoxCastAll(createPos.position, transform.lossyScale / 2f, transform.forward, transform.rotation, skillState.range);

        for (int i = 0; i < hits.Length; i++)
        {
            IHit hitObj = hits[i].collider.GetComponent<IHit>();
            if (hitObj != null)
            {
                if (hits[i].collider.gameObject.name.CompareTo("Boss") == 0)
                    continue;

                hitObj.TakeDamage(skillState.damage, true);
            }
        }

        StartCoroutine(enemy.CoolTimeRoutine(skillState.atkAble, skillState.coolTime));
        StartCoroutine(enemy.CoolTimeRoutine(globalState.Able, globalState.coolTime.Value));

        GameObject obj = GameObject.Instantiate(electricZone, createPos.position, createPos.rotation);
        obj.transform.parent = createPos;
        return TaskStatus.Success;
    }
}