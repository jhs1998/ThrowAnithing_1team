using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPrefabs : MonoBehaviour
{
    [SerializeField] GameObject prefabs;
    [SerializeField] List<Transform> spawnPos;
    [SerializeField] int spwanCount;
    Transform[] childCube;

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
                
                int num = Random.Range(0, spawnPos.Count - 1);
                Instantiate(prefabs, spawnPos[num]);
                
                if (prefabs.tag == "Trash")
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




