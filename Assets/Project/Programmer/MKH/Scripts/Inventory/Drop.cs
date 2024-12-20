using MKH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Drop : MonoBehaviour
{
    public ItemDripTable dropList;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            DropItem();
    }

    private void DropItem()
    {
        dropList.ItemDrop(transform.position);
        
    }
}
