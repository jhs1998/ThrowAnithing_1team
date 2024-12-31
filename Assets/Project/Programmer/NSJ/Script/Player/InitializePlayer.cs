using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePlayer : MonoBehaviour
{
    void Start()
    {
        Inventory.Instance.Controller.ItemReset();
    }
}
