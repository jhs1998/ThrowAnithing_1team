using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject _Inventory;
        [SerializeField] GameObject _EquipInventory;
        [SerializeField] GameObject _State;
        [SerializeField] GameObject _BlueChipPanel;

        private void Awake()
        {
            _Inventory = GameObject.Find("InventoryUI").transform.Find("Inventory").gameObject;
            _EquipInventory = GameObject.Find("InventoryUI").transform.Find("EquipmentInventory").gameObject;
            _State = GameObject.Find("InventoryUI").transform.Find("State").gameObject;
            _BlueChipPanel = GameObject.Find("InventoryUI").transform.Find("BlueChipPanel").gameObject;
        }

        private void Start()
        {
            _Inventory.SetActive(false);
            _EquipInventory.SetActive(false);
            _BlueChipPanel.SetActive(false);
            _State.SetActive(false);
        }

        private void Update()
        {
            OpenBlueChip();
            Inventory();
            CloseBlueChip();
        }

        private void Inventory()
        {
            if (InputKey.GetButtonDown("Inventory"))
            {
                if (_Inventory.activeSelf)
                    return;

                _Inventory.SetActive(true);
                _EquipInventory.SetActive(true);
                _State.SetActive(true);
            }

            if (InputKey.GetButtonDown("Negative"))
            {
                if (_BlueChipPanel.activeSelf)
                    return;

                _Inventory.SetActive(false);
                _EquipInventory.SetActive(false);
                _State.SetActive(false);
            }
        }

        private void OpenBlueChip()
        {
            if (!_Inventory.activeSelf)
                return;

            if (InputKey.GetButtonDown("Inventory"))
            {
                _BlueChipPanel.SetActive(true);
            }
        }

        private void CloseBlueChip()
        {
            if (!_Inventory.activeSelf)
                return;

            if (InputKey.GetButtonDown("PopUp Close"))
            {
                _BlueChipPanel.SetActive(false);
            }
        }
    }
}
