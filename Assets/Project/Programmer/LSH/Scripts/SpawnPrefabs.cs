using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPrefabs : MonoBehaviour
{
    [SerializeField] GameObject prefabs; //스폰될 프리팹
    [SerializeField] List<Transform> spawnPos; //스폰 지점들
    [SerializeField] int spwanCount; //지점들 중에서 몇개를 스폰시킬지
    Transform[] childCube; //prefabs가 Tag.Trash일때의 자식들
    [SerializeField] bool isAlreadySpawn;


    private void Start()
    {

        childCube = prefabs.GetComponentsInChildren<Transform>(true);
                
        SpawnPrefabPos();        
    

    }


    public void SpawnPrefabPos()
    {

        if (spawnPos.Count < spwanCount)
        {
            return;
        }
        else
        {                      

            while (spwanCount != 0)
            {
                
                int num = Random.Range(0, spawnPos.Count);
                Instantiate(prefabs, spawnPos[num]);
                
                if (prefabs.tag == Tag.Trash)
                {
                    SpawnTrashPos();
                }

                spawnPos.Remove(spawnPos[num]);
                spwanCount -= 1;

            }
        }


    }


    public void SpawnTrashPos()
    {

        for (int i = 0; i < childCube.Length; i++)
        {
            if (childCube[i] == prefabs.transform)
                childCube[i].gameObject.SetActive(true);
            else
                childCube[i].gameObject.SetActive(Random.value > 0.5f);

        }

        
    }

}




