using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : BaseUI
{
    GameObject continueImage;
    GameObject newImage;
    GameObject optionImage;
    GameObject exitImage;

    //메뉴 버튼 배열
    GameObject[] menuButtons;

    //메뉴 선택하는 배열의 인덱스(현재 선택된 메뉴)
    int curMenu;
    private void Awake()
    {
        Bind();
    }

    private void Start()
    {
        Init();

    }

    private void Update()
    {
        MenuSelect();
        SelectedEnter();
    }

    // Comment : 초기화 용도의 함수
    void Init()
    {
        menuButtons = new GameObject[4];
        menuButtons[0] = continueImage = GetUI("ContinueImage");
        menuButtons[1] = newImage = GetUI("NewImage");
        menuButtons[2] = optionImage = GetUI("OptionImage");
        menuButtons[3] = exitImage = GetUI("ExitImage");

        curMenu = 0;
    }

    // Comment : W/S 또는 위/아래 화살표 키를 이용해 메뉴 변경 가능 함수
    // Todo : 더 효율적인 방법이 있는지 질문해보기
    private void MenuSelect()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            menuButtons[curMenu].gameObject.SetActive(false);

            if (curMenu == 3)
            {
                curMenu = 0;
                menuButtons[curMenu].gameObject.SetActive(true);
                return;
            }

            curMenu++;
            menuButtons[curMenu].gameObject.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            menuButtons[curMenu].gameObject.SetActive(false);

            if (curMenu == 0)
            {
                curMenu = 3;
                menuButtons[curMenu].gameObject.SetActive(true);
                return;
            }

            curMenu--;
            menuButtons[curMenu].gameObject.SetActive(true);
        }
    }

    //Comment : 선택한 메뉴로 진입하는 함수
    void SelectedEnter()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (curMenu)
            {
                case 0:
                    Debug.Log("계속하기 선택_슬롯 선택 팝업 노출");
                    //Todo : 슬롯 선택 팝업 만들어야함 > 백엔드와 협업 필요할 듯
                    break;

                case 1:
                    Debug.Log("새로하기 선택_게임 시작 화면으로 이동");
                    //Todo : 게임 화면으로 이동 만들어야함
                    break;

                case 2:
                    Debug.Log("옵션 선택_옵션 팝업 노출");
                    //Todo : 옵션 팝업 만들어야함
                    break;

                case 3:
                    Debug.Log("게임 종료 선택_게임 종료");
                    //Todo : 게임종료 만들어야함
                    break;
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
            Debug.Log("게임 종료 선택_게임 종료");
        //Todo : 게임종료 만들어야함
    }

}
