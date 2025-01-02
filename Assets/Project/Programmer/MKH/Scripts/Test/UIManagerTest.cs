using UnityEngine;

namespace MKH
{
    public class UIManagerTest : MonoBehaviour
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
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (_Inventory.activeSelf)
                    return;

                Time.timeScale = 0;
                _Inventory.SetActive(true);
                _EquipInventory.SetActive(true);
                _State.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                if (_BlueChipPanel.activeSelf)
                    return;

                Time.timeScale = 1;
                _Inventory.SetActive(false);
                _EquipInventory.SetActive(false);
                _State.SetActive(false);
            }
        }

        private void OpenBlueChip()
        {
            if (!_Inventory.activeSelf)
                return;

            if (Input.GetKeyDown(KeyCode.B))
            {
                _BlueChipPanel.SetActive(true);
            }
        }

        private void CloseBlueChip()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                _BlueChipPanel.SetActive(false);
            }

        }
    }
}
