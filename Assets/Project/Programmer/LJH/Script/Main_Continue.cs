using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Main_Continue : MainScene
{
    GameObject[] slots;

    GameObject slot1;
    GameObject slot2;
    GameObject slot3;

    int slots_cur;


    void Start()
    {
        Init();
        SlotPill();
    }

    void Update()
    {
        slots_Select();
        SelectedEnter();
    }

    private void slots_Select()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            slots[slots_cur].GetComponent<Outline>().effectDistance = new(0, 0);

            if (slots_cur == slots.Length-1)
            {
                slots_cur = 0;
                slots[slots_cur].GetComponent<Outline>().effectDistance = new(10, 10);
                return;
            }

            slots_cur++;
            slots[slots_cur].GetComponent<Outline>().effectDistance = new(10, 10);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            slots[slots_cur].GetComponent<Outline>().effectDistance = new(0, 0);

            if (slots_cur == 0)
            {
                slots_cur = slots.Length-1;
                slots[slots_cur].GetComponent<Outline>().effectDistance = new(10, 10);
                return;
            }

            slots_cur--;
            slots[slots_cur].GetComponent<Outline>().effectDistance = new(10, 10);
        }
    }

    void SelectedEnter()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (slots_cur)
            {
                case 0:
                    Debug.Log("1번 슬롯으로 게임 진행");
                    //Todo : 슬롯 선택 팝업 만들어야함 > 백엔드와 협업 필요할 듯
                    break;

                case 1:
                    Debug.Log("2번 슬롯으로 게임 진행");
                    //Todo : 게임 화면으로 이동 만들어야함
                    break;

                case 2:
                    Debug.Log("3번 슬롯으로 게임 진행");
                    //Todo : 옵션 팝업 만들어야함
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            Debug.Log("이어하기 화면 나가기");
        }
    }

    void SlotPill()
    {
        //Todo : 세이브 슬롯 있을 경우 슬롯 채워야함
        slot1.GetComponentInChildren<TMP_Text>().text = "Empty";
        slot2.GetComponentInChildren<TMP_Text>().text = "Empty";
        slot3.GetComponentInChildren<TMP_Text>().text = "Empty";
    }

    private void Init()
    {
        
        slots = new GameObject[3];

        slots[0] = slot1 = GetUI("FirstSlot");
        slots[1] = slot2 = GetUI("SecondSlot");
        slots[2] = slot3 = GetUI("ThirdSlot");

        slots_cur = 0;
    }

}
