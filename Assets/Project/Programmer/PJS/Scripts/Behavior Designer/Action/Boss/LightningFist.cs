using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class LightningFist : Action
{
    [SerializeField] BossSkillState skillState;
    [SerializeField] GlobalState globalState;
    [SerializeField] Transform createPos;

    private BossEnemy enemy;
    private RaycastHit[] hits;

    public override void OnStart()
    {
        enemy = GetComponent<BossEnemy>();
    }

    public override TaskStatus OnUpdate()
    {
        hits = Physics.BoxCastAll(createPos.position, transform.lossyScale / 2f, transform.forward, transform.rotation, skillState.range);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.name.CompareTo("Boss") == 0)
                continue;

            //enemy.Battle.TargetAttack(hits[i].transform, skillState.damage);
            enemy.Battle.TargetAttackWithDebuff(hits[i].transform, skillState.damage);
            enemy.Battle.TargetCrowdControl(hits[i].transform, CrowdControlType.Stiff);
        }

        StartCoroutine(enemy.CoolTimeRoutine(skillState.atkAble, skillState.coolTime));
        StartCoroutine(enemy.CoolTimeRoutine(globalState.Able, globalState.coolTime.Value));

        return TaskStatus.Success;
    }
}