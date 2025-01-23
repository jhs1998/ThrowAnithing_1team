using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPrefabs : MonoBehaviour
{
    [SerializeField] GameObject prefabs; //������ ������
    [SerializeField] List<Transform> spawnPos; //���� ������
    [SerializeField] int spwanCount; //������ �߿��� ��� ������ų��

    [SerializeField] int trashCount; // ������ � ����?
    ThrowObject[] childCube; //prefabs�� Tag.Trash�϶��� �ڽĵ�
    [SerializeField] bool isAlreadySpawn;

    private void Start()
    {
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
                GameObject instance = Instantiate(prefabs, spawnPos[num]);

                if (prefabs.tag == Tag.Trash)
                {
                    SpawnTrashPos(instance);
                }

                spawnPos.Remove(spawnPos[num]);
                spwanCount -= 1;

            }
        }


    }


    public void SpawnTrashPos(GameObject instance)
    {

        //for (int i = 0; i < childCube.Length; i++)
        //{
        //    if (childCube[i] == prefabs.transform)
        //        childCube[i].gameObject.SetActive(true);
        //    else
        //        childCube[i].gameObject.SetActive(Random.value > 0.5f);

        //}

        childCube = instance.GetComponentsInChildren<ThrowObject>(true);

        if (trashCount >= childCube.Length)
        {
            trashCount = childCube.Length;
        }

        foreach (ThrowObject child in childCube) 
        {
            child.gameObject.SetActive(false);
        }


        for (int i = 0; i < trashCount; i++)
        {
            while (true)
            {
                int randomNum = Random.Range(0, childCube.Length);

                if (childCube[randomNum].gameObject.activeSelf == true) 
                {
                    continue;
                }
                else
                {
                    childCube[randomNum].gameObject.SetActive (true);
                    break;
                }
            }


        }


    }

}




