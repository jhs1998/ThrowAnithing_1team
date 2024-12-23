using MKH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Drop : MonoBehaviour
{
    public DropList dropList;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            DropItem();
    }

    private void DropItem()
    {
        GameObject dropPrefab = dropList.itemList[Random.Range(0, dropList.Count)];
        Instantiate(dropPrefab, transform.position, Quaternion.identity);

    }
}
