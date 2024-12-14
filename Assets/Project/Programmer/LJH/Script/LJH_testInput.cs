using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJH_testInput : MonoBehaviour
{
    public GameObject inventory;

    private void Start()
    {
        inventory.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventory.activeSelf)
            {
                inventory.SetActive(true);
                Debug.Log("인벤토리 열림");
                return;
            }
            if (inventory.activeSelf)
            {
                inventory.SetActive(false);
                Debug.Log("인벤토리 닫힘");
                return;
            }
        }

    }
}
