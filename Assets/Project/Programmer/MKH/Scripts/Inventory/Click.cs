using UnityEngine;
using UnityEngine.UI;

namespace MKH
{
    public class Click : MonoBehaviour
    {
        [SerializeField] GameObject[] buttons;
        [SerializeField] GameObject mInventorySlotsParent;
        [SerializeField] InventorySlot[] ivSlots;
        [SerializeField] GameObject mEquipmentSlotsParent;
        [SerializeField] InventorySlot[] eqSlots;
        int selectedButtonsIndex = 9;
        int buttonCount;
        private bool axisInUse = false;

        [SerializeField] Color HighlightedColor;
        [SerializeField] Color color;

        private void Awake()
        {
            ivSlots = mInventorySlotsParent.GetComponentsInChildren<InventorySlot>();
            eqSlots = mEquipmentSlotsParent.GetComponentsInChildren<InventorySlot>();
        }

        private void Start()
        {
            buttonCount = buttons.Length;
            axisInUse = false;
        }

        private void Update()
        {
            ButtonsControl();
            Function();
        }

        #region 키 조작
        // 키보드 조작
        private void ButtonsControl()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            if (x == -1)        // 좌측
            {
                if (selectedButtonsIndex > 0 && axisInUse == false)
                {
                    axisInUse = true;
                    selectedButtonsIndex -= 1;
                    Debug.Log("좌");
                }
            }
            else if (x == 1)    // 우측
            {
                if (selectedButtonsIndex < buttons.Length - 1 && axisInUse == false)
                {
                    axisInUse = true;
                    selectedButtonsIndex += 1;
                    Debug.Log("우");
                }
            }
            else if (y == 1)    // 위
            {
                if (selectedButtonsIndex > 2 && axisInUse == false)
                {
                    axisInUse = true;
                    selectedButtonsIndex -= 3;
                    Debug.Log("위");
                }
            }
            else if (y == -1)   // 아래
            {
                if (selectedButtonsIndex < buttons.Length - 3 && axisInUse == false)
                {
                    axisInUse = true;
                    selectedButtonsIndex += 3;
                    Debug.Log("아래");
                }
            }
            else
            {
                axisInUse = false;
            }

            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == selectedButtonsIndex)
                {
                    buttons[i].GetComponent<Image>().color = HighlightedColor;
                }
                else
                {
                    buttons[i].GetComponent<Image>().color = color;
                }
            }
        }
        #endregion

        #region 아이템 사용 버튼
        private void Use(int index)
        {
            // 아이템 장착
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (index < 9)
                {
                    return;
                }
                else if(index >= 9)
                {
                    ivSlots[index - 9].UseItem();
                    Debug.Log($"{index - 9}버튼 누름");
                    Debug.Log($"{index - 9}번 장착");
                }
            }

            // 아이템 삭제
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (index < 9)
                {
                    eqSlots[index].RemoveEquipmentSlot();
                    Debug.Log($"{index}버튼 누름");
                    Debug.Log($"{index}번 장비 삭제");

                }
                else if (index >= 9)
                {
                    ivSlots[index - 9].ClearSlot();
                    Debug.Log($"{index - 9}버튼 누름");
                    Debug.Log($"{index - 9}번 삭제");
                }
            }
        }

        private void Function()
        {
            Use(selectedButtonsIndex);
        }
        #endregion
    }
}
