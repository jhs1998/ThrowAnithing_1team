using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class LightningFist : Action
{
    [SerializeField] BossSkillState skillState;
    [SerializeField] GlobalState globalState;
    [SerializeField] GameObject electricZone;
    [SerializeField] Transform createPos;
    [SerializeField] SharedFloat speed;
    [SerializeField] string animName;

    private float preSpeed;
    private BossEnemy enemy;
    private Animator anim;

    public override void OnStart()
    {
        enemy = GetComponent<BossEnemy>();
        anim = GetComponent<Animator>();
        preSpeed = speed.Value;
    }

    public override void OnEnd()
    {
        speed.SetValue(preSpeed);
    }

    public override TaskStatus OnUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(animName) == true)
        {
            speed.Value = 0;
            Debug.Log($"{speed.Value} ¼Óµµ");
        }
        else 
            Debug.Log("fail");

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

        GameObject.Instantiate(electricZone, createPos.position, createPos.rotation);
        return TaskStatus.Success;
    }
}