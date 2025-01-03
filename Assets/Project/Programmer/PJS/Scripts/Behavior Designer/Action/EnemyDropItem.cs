using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : Action
{
    [SerializeField] SharedFloat reward;
    [SerializeField] int maxPersent;
    [SerializeField] List<GameObject> dropItems;
    public override void OnStart()
    {
        if (reward.Value > maxPersent)
            reward.Value = reward.Value / maxPersent;
    }

    public override TaskStatus OnUpdate()
    {
        // 몬스터가 죽었을 시 아이템 드랍
        //int randNum = Random.Range(0, maxPersent+1);
        //Debug.Log(randNum);
        //GameObject.Instantiate(DataContainer.GetItemPrefab(), transform.position + new Vector3(0, 1, 0), transform.rotation);
        GameObject.Instantiate(dropItems[0], transform.position + new Vector3(0, 1, 0), transform.rotation);

        // TODO : 확률 인스펙터에서 정해서 값 가져오기
        /*if (randNum <= reward.Value)
        {
            if (dropItems == null)
            {
                return TaskStatus.Success;
            }


        }*/

        return TaskStatus.Success;
    }
}