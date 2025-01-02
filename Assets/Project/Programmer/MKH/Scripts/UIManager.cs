using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    public class UIManager : BaseBinder
    {
        [SerializeField] GameObject _Inventory;
        [SerializeField] GameObject _EquipInventory;
        [SerializeField] GameObject _State;
        [SerializeField] GameObject _BlueChipPanel;
        [SerializeField] GameObject _BlueChipChoice;

        private void Awake()
        {
            Bind();
            //_Inventory = GameObject.Find("InventoryUI").transform.Find("Inventory").gameObject;
            //_EquipInventory = GameObject.Find("InventoryUI").transform.Find("EquipmentInventory").gameObject;
            //_State = GameObject.Find("InventoryUI").transform.Find("State").gameObject;
            //_BlueChipPanel = GameObject.Find("InventoryUI").transform.Find("BlueChipPanel").gameObject;
            _Inventory = GetObject("Inventory");
            _EquipInventory = GetObject("EquipmentInventory");
            _State = GetObject("State");
            _BlueChipPanel = GetObject("BlueChipPanel");
            _BlueChipChoice = GetObject("BlueChipChoice");

            _Inventory.SetActive(false);
            _EquipInventory.SetActive(false);
            _State.SetActive(false);
            _BlueChipChoice.SetActive(false);
            _BlueChipPanel.SetActive(false);
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
            if (InputKey.GetButtonDown(InputKey.Inventory))
            {
                if (_Inventory.activeSelf)
                    return;

                _Inventory.SetActive(true);
                _EquipInventory.SetActive(true);
                _State.SetActive(true);
            }

            if (InputKey.GetButtonDown(InputKey.PopUpClose))
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

            if (InputKey.GetButtonDown(InputKey.Inventory))
            {
                _BlueChipPanel.SetActive(true);
            }
        }

        private void CloseBlueChip()
        {
            if (!_Inventory.activeSelf)
                return;

            if (InputKey.GetButtonDown(InputKey.PopUpClose))
            {
                _BlueChipPanel.SetActive(false);
            }
        }
    }
}
