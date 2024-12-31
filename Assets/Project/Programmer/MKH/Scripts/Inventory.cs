using MKH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [SerializeField] public EquipmentInventory EquipInventory;
    [SerializeField] public InventoryMain InventoryMain;
    [SerializeField] public GameObject BlueChipChoice;
    [SerializeField] public BlueChipPanel BlueChipPanel;

    [Inject]
    PlayerData playerData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        playerData.Inventory.Inventory = EquipInventory;
        playerData.Inventory.InventoryMain = InventoryMain;
        playerData.Inventory.BlueChipChoice = BlueChipChoice;
        playerData.Inventory.BlueChipPanel = BlueChipPanel;
    }


    private void InitSingleTon()
    {

    }
}
