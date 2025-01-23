using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MKH
{
    public class UIManagerTest : MonoBehaviour
    {
        [SerializeField] GameObject _Inventory;
        [SerializeField] GameObject _EquipInventory;
        [SerializeField] GameObject _State;
        [SerializeField] GameObject _BlueChipPanel;
        InventorySlotTest[] ivSlots;
        InventorySlotTest[] eqSlots;
        InventoryMainTest _Main;
        [SerializeField] GameObject[] buttons;
        int selectedButtonsIndex;
        int buttonCount;

        [Header("������ ����")]
        [SerializeField] TMP_Text ivName;                       // �κ��丮 ������ �̸�
        [SerializeField] TMP_Text ivDescription;                // �κ��丮 ������ ����
        [SerializeField] TMP_Text eqName;                       // ��� ������ �̸�
        [SerializeField] TMP_Text eqDescription;                // ��� ������ ����

        private void Awake()
        {
            _Inventory = GameObject.Find("InventoryUI").transform.Find("Inventory").gameObject;
            _EquipInventory = GameObject.Find("InventoryUI").transform.Find("EquipmentInventory").gameObject;
            _State = GameObject.Find("InventoryUI").transform.Find("State").gameObject;
            _BlueChipPanel = GameObject.Find("InventoryUI").transform.Find("BlueChipPanel").gameObject;
            ivSlots = _Inventory.GetComponentsInChildren<InventorySlotTest>();
            eqSlots = _EquipInventory.GetComponentsInChildren<InventorySlotTest>();
            _Main = GetComponent<InventoryMainTest>();
        }

        private void Start()
        {
            _Inventory.SetActive(false);
            _EquipInventory.SetActive(false);
            _BlueChipPanel.SetActive(false);
            _State.SetActive(false);

            buttonCount = buttons.Length;
        }

        private void Update()
        {
            OpenBlueChip();
            Inventory();
            CloseBlueChip();
        }

        #region �κ��丮, ���, ���Ĩ ���� �ݱ�
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
        #endregion

        public void Click()
        {
            //if (input.GetKeyDown(KeyCode.E))
            //{
            //    _Slot.UseItem();
            //    _Main.Sorting();
            //}
            //if (input.GetKeyDown(KeyCode.E))
            //{
            //for (int i = 0; i < _Slot.Length; i++)
            //{
            //    _Slot[i].UseItem();
            //    _Main.Sorting();
            //    return;
            //}
            // }
        }

        #region ������ ��ư ����
        private void Use(int index)
        {
            if (_Inventory.activeSelf && !_BlueChipPanel.activeSelf)
            {
                // ������ ���� - �κ��丮�� ���� ����
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // �κ��丮
                    if (index >= 9)
                    {
                        if (ivSlots[index - 9].Item != null)
                        {
                            ivSlots[index - 9].UseItem();
                            _Main.Sorting();
                            Debug.Log($"�κ��丮 {index - 9}�� ��� ����");
                        }
                        else if (ivSlots[index - 9].Item == null)
                        {
                            Debug.Log("���� �� ��� �����ϴ�.");
                            return;
                        }
                    }
                }

                // ������ ���� - �κ��丮, ��� �� �� ���� ����
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    // ���
                    //if (index < 9)
                    //{
                    //    if (eqSlots[index].Item != null)
                    //    {
                    //        eqSlots[index].RemoveEquipmentSlot();
                    //        Debug.Log($"��� {index}�� ����");
                    //    }
                    //    else if(eqSlots[index].Item == null)
                    //    {
                    //        Debug.Log("���� �� ��� �����ϴ�");
                    //        return;
                    //    }

                    //}
                    // �κ��丮
                    if (index >= 9)
                    {
                        if (ivSlots[index - 9].Item != null)
                        {
                            ivSlots[index - 9].ClearSlot();
                            _Main.Sorting();
                            Debug.Log($"�κ��丮 {index - 9}�� ��� ����");
                        }
                        else if (ivSlots[index - 9].Item == null)
                        {
                            Debug.Log("���� �� ��� �����ϴ�.");
                            return;
                        }
                    }
                }
            }
        }
        #endregion

        #region ������ ����
        private void Info()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == selectedButtonsIndex)
                {
                    // ���â ����
                    if (i < 9)
                    {
                        // ��� ���Կ� �������� �ִ� ����
                        if (eqSlots[i].Item != null)
                        {
                            eqName.text = eqSlots[i].Item.Name;
                            eqDescription.text = eqSlots[i].Item.Description;
                            ivName.text = "-";
                            ivDescription.text = "";
                        }
                        // ��� ���Կ� �������� ���� ����
                        else if (eqSlots[i].Item == null)
                        {
                            eqName.text = "-";
                            eqDescription.text = "";
                            ivName.text = "-";
                            ivDescription.text = "";
                        }
                    }
                    // �κ��丮 ����
                    else if (i >= 9)
                    {
                        // �κ��丮 ���Կ� ������ �ִ� ����
                        if (ivSlots[i - 9].Item != null)
                        {
                            ivName.text = ivSlots[i - 9].Item.Name;
                            ivDescription.text = ivSlots[i - 9].Item.Description;

                            // ������ Ÿ�� �� �κ��丮�� ��� ��
                            switch (ivSlots[i - 9].Item.Type)
                            {
                                case ItemType.Helmet:
                                    if (eqSlots[0].Item != null)
                                    {
                                        eqName.text = eqSlots[0].Item.Name;
                                        eqDescription.text = eqSlots[0].Item.Description;
                                    }
                                    else if (eqSlots[0].Item == null)
                                    {
                                        eqName.text = "-";
                                        eqDescription.text = "";
                                    }
                                    break;
                                case ItemType.Shirts:
                                    if (eqSlots[1].Item != null)
                                    {
                                        eqName.text = eqSlots[1].Item.Name;
                                        eqDescription.text = eqSlots[1].Item.Description;
                                    }
                                    else if (eqSlots[1].Item == null)
                                    {
                                        eqName.text = "-";
                                        eqDescription.text = "";
                                    }
                                    break;
                                case ItemType.Glasses:
                                    if (eqSlots[2].Item != null)
                                    {
                                        eqName.text = eqSlots[2].Item.Name;
                                        eqDescription.text = eqSlots[2].Item.Description;
                                    }
                                    else if (eqSlots[2].Item == null)
                                    {
                                        eqName.text = "-";
                                        eqDescription.text = "";
                                    }
                                    break;
                                case ItemType.Gloves:
                                    if (eqSlots[3].Item != null)
                                    {
                                        eqName.text = eqSlots[3].Item.Name;
                                        eqDescription.text = eqSlots[3].Item.Description;
                                    }
                                    else if (eqSlots[3].Item == null)
                                    {
                                        eqName.text = "-";
                                        eqDescription.text = "";
                                    }
                                    break;
                                case ItemType.Pants:
                                    if (eqSlots[4].Item != null)
                                    {
                                        eqName.text = eqSlots[4].Item.Name;
                                        eqDescription.text = eqSlots[4].Item.Description;
                                    }
                                    else if (eqSlots[4].Item == null)
                                    {
                                        eqName.text = "-";
                                        eqDescription.text = "";
                                    }
                                    break;
                                case ItemType.Earring:
                                    if (eqSlots[5].Item != null)
                                    {
                                        eqName.text = eqSlots[5].Item.Name;
                                        eqDescription.text = eqSlots[5].Item.Description;
                                    }
                                    else if (eqSlots[5].Item == null)
                                    {
                                        eqName.text = "-";
                                        eqDescription.text = "";
                                    }
                                    break;
                                case ItemType.Ring:
                                    if (eqSlots[6].Item != null)
                                    {
                                        eqName.text = eqSlots[6].Item.Name;
                                        eqDescription.text = eqSlots[6].Item.Description;
                                    }
                                    else if (eqSlots[6].Item == null)
                                    {
                                        eqName.text = "-";
                                        eqDescription.text = "";
                                    }
                                    break;
                                case ItemType.Shoes:
                                    if (eqSlots[7].Item != null)
                                    {
                                        eqName.text = eqSlots[7].Item.Name;
                                        eqDescription.text = eqSlots[7].Item.Description;
                                    }
                                    else if (eqSlots[7].Item == null)
                                    {
                                        eqName.text = "-";
                                        eqDescription.text = "";
                                    }
                                    break;
                                case ItemType.Necklace:
                                    if (eqSlots[8].Item != null)
                                    {
                                        eqName.text = eqSlots[8].Item.Name;
                                        eqDescription.text = eqSlots[8].Item.Description;
                                    }
                                    else if (eqSlots[8].Item == null)
                                    {
                                        eqName.text = "-";
                                        eqDescription.text = "";
                                    }
                                    break;
                            }
                        }
                        // �κ��丮 ���Կ� �������� ���� ����
                        else if (ivSlots[i - 9].Item == null)
                        {
                            ivName.text = "-";
                            ivDescription.text = "";
                            eqName.text = "-";
                            eqDescription.text = "";
                        }
                    }
                }
            }
        }
        #endregion
    }
}
