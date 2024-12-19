using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum CurState
{
    main,
    _continue,
    optionDepth1,
    optionDepth2,
};
public class MainScene : BaseUI
{
    public CurState curState { get; protected set; }

    //메인 화면
    protected GameObject main;
    //메뉴 버튼
    GameObject[] menuButtons;

    GameObject continueImage;
    GameObject newImage;
    GameObject optionImage;
    GameObject exitImage;

    //이어하기 패널
    GameObject main_continue;

    //옵션 패널
    GameObject option;

    //나가기 팝업
    GameObject exitPopUpObj;
    GameObject[] exitButtons;

    GameObject exitYes;
    GameObject exitNo;

    int exitNum;

    //메뉴 선택하는 배열의 인덱스(현재 선택된 메뉴)
    int curMenu;

    protected bool isOption;
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
        if (!option.activeSelf && !main_continue.activeSelf)
            curState = CurState.main;

        Debug.Log(curState);
        if (curState == CurState.main)
        {
            MenuSelect();

            SelectedEnter();

            if (exitPopUpObj.activeSelf)
                ExitPopUp();
        }

        
    }

    // Comment : 초기화 용도의 함수
    void Init()
    {
        main = GetUI("MainButtons");
       // curState = CurState.main;

        menuButtons = new GameObject[4];
        menuButtons[0] = continueImage = GetUI("ContinueImage");
        menuButtons[1] = newImage = GetUI("NewImage");
        menuButtons[2] = optionImage = GetUI("OptionImage");
        menuButtons[3] = exitImage = GetUI("ExitImage");

        exitButtons = new GameObject[2];
        exitButtons[0] = exitYes = GetUI("YesImage");
        exitButtons[1] = exitNo = GetUI("NoImage");

        exitPopUpObj = GetUI("ExitPopUp");

        main_continue = GetUI("Background_continue");

        option = GetUI("Background_option");
        
        
        curMenu = 0;
        exitNum = 0;

        
    }

    // Comment : W/S 또는 위/아래 화살표 키를 이용해 메뉴 변경 가능 함수
    // Todo : 더 효율적인 방법이 있는지 질문해보기
    private void MenuSelect()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            menuButtons[curMenu].SetActive(false);

            if (curMenu == menuButtons.Length-1)
            {
                curMenu = 0;
                menuButtons[curMenu].SetActive(true);
                return;
            }

            curMenu++;
            menuButtons[curMenu].SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            menuButtons[curMenu].SetActive(false);

            if (curMenu == 0)
            {
                curMenu = menuButtons.Length - 1;
                menuButtons[curMenu].SetActive(true);
                return;
            }

            curMenu--;
            menuButtons[curMenu].SetActive(true);
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
                    main_continue.SetActive(true);
                    curState = CurState._continue;
                    break;

                case 1:
                    Debug.Log("새로하기 선택_게임 시작 화면으로 이동");
                    //Todo : 게임 화면으로 이동 만들어야함
                    break;

                case 2:
                    Debug.Log("옵션 선택_옵션 팝업 노출");
                    option.SetActive(true);
                    curState = CurState.optionDepth1;
                    //Todo : 옵션 팝업 만들어야함
                    break;

                case 3:
                    Debug.Log("게임 종료 선택_게임 종료");
                    exitPopUpObj.SetActive(true);
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitPopUpObj.SetActive(true);
            Debug.Log("게임 종료 선택_게임 종료");
        }
    }

    void ExitPopUp()
    {

        switch (exitNum)
        {
            case 0:
                exitButtons[exitNum].GetComponent<Image>().color = Color.black;
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    exitButtons[exitNum].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                    exitNum = 1;
                }
                if (Input.GetKeyDown(KeyCode.E))
                    ExitGame();
                break;

            case 1:
                exitNo.GetComponent<Image>().color = Color.black;
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    exitNo.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                    exitNum = 0;
                }
                if (Input.GetKeyDown(KeyCode.E))
                    exitPopUpObj.SetActive(false);
                break;

        }
    }
    void ExitGame()
    {
        //Comment : 유니티 에디터상에서 종료
        UnityEditor.EditorApplication.isPlaying = false;
        //Comment : 빌드 상에서 종료
        Application.Quit();
    }

}
