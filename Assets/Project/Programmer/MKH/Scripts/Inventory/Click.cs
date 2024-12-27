using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MKH
{
    public class Click : MonoBehaviour
    {
        [SerializeField] GameObject[] buttons;
        [SerializeField] GameObject mInventorySlotsParent;
        [SerializeField] InventorySlot[] slots;
        int selectedButtonsIndex = 0;
        int buttonCount;
        private bool axisInUse = false;

        [SerializeField] Color HighlightedColor;
        [SerializeField] Color color;

        private void Awake()
        {
            slots = mInventorySlotsParent.GetComponentsInChildren<InventorySlot>();
        }

        private void Start()
        {
            buttonCount = buttons.Length;

        }

        private void Update()
        {
            ButtonsControl();
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

            if (Input.GetKeyDown(KeyCode.E))
            {
                Function();
            }
        }
        #endregion

        #region 버튼 입력
        private void Function()
        {
            switch (selectedButtonsIndex)
            {
                case 0:
                    print("1번 버튼");
                    slots[0].UseItem();
                    break;
                case 1:
                    print("2번 버튼");
                    slots[1].UseItem();
                    break;
                case 2:
                    print("3번 버튼");
                    slots[2].UseItem();
                    break;
                case 3:
                    print("4번 버튼");
                    slots[3].UseItem();
                    break;
                case 4:
                    print("5번 버튼");
                    slots[4].UseItem();
                    break;
                case 5:
                    print("6번 버튼");
                    slots[5].UseItem();
                    break;
                case 6:
                    print("7번 버튼");
                    slots[6].UseItem();
                    break;
                case 7:
                    print("8번 버튼");
                    slots[7].UseItem();
                    break;
                case 8:
                    print("9번 버튼");
                    slots[8].UseItem();
                    break;
                case 9:
                    print("10번 버튼");
                    slots[9].UseItem();
                    break;
                case 10:
                    print("11번 버튼");
                    slots[10].UseItem();
                    break;
                case 11:
                    print("12번 버튼");
                    slots[11].UseItem();
                    break;
            }
        }
    }
    #endregion
}
