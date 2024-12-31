using MKH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [SerializeField] EquipmentInventory _inventory;
    [SerializeField] InventoryMain _inventoryMain;
    [SerializeField] GameObject _blueChipChoice;
    [SerializeField] BlueChipPanel _blueChipPanel;

    [Inject]
    PlayerData playerData;

    private void Awake()
    {
        InitSingleTon();
        playerData.Inventory.Inventory = _inventory;
        playerData.Inventory.InventoryMain = _inventoryMain;
        playerData.Inventory.BlueChipChoice = _blueChipChoice;
        playerData.Inventory.BlueChipPanel = _blueChipPanel;
    }


    private void InitSingleTon()
    {
        if(Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
