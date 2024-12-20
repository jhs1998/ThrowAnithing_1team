using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DropList : ScriptableObject
{
    public List<GameObject> itemList;

    public GameObject this[int index]
    {
        get
        {
            return itemList[index];
        }
    }

    public int Count => itemList.Count;
}
