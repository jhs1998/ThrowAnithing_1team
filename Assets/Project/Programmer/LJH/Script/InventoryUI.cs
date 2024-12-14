using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : BaseUI
{
    GameObject pocket1;

    public void Awake()
    {
        Bind();

    }
    public void Start()
    {
        pocket1 = GetUI("pocket1");
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            pocket1.SetActive(false);
        }
    }

}
