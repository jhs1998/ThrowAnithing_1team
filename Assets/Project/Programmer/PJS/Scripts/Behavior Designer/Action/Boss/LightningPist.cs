using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class LightningPist : Action
{
    [SerializeField] float range;
    [SerializeField] int damage;

    public override void OnStart()
    {

    }

    public override TaskStatus OnUpdate()
    {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, transform.lossyScale / 2f, transform.forward, transform.rotation, range);

        for (int i = 0; i < hits.Length; i++)
        {
            IHit hitObj = hits[i].collider.GetComponent<IHit>();
            if (hitObj != null)
            {
                Debug.Log(hits[i].collider.gameObject);
                if (hits[i].collider.gameObject.name.CompareTo("Boss") == 0)
                    continue;

                hitObj.TakeDamage(damage, true);
            }
        }

        return TaskStatus.Success;
    }
}