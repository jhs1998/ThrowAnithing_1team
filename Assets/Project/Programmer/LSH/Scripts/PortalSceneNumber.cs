using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSceneNumber : MonoBehaviour
{
    [SerializeField] public SceneField nextScene;
    [SerializeField] public SceneField[] hiddenSceneArr;

    [SerializeField] GameObject[] inventory;

    private void Start()
    {
        for(int i = 0; i < inventory.Length; i++)
            inventory[i].SetActive(false);

        ToStage._ToStage();
    }


}
