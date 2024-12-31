using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum CurState
{
    main,
    _continue,
    _new,
    optionDepth1,
    optionDepth2,
};
public class MainScene : BaseUI
{
    public CurState curState { get; protected set; }

    //메인 화면
    protected GameObject main;
    //메뉴 버튼
    Button[] menuButtons;

    Button continueImage;
    Button newImage;
    Button optionImage;
    Button exitImage;

    //이어하기 패널
    GameObject main_continue;

    //새로하기
    GameObject main_new;

    //옵션 패널
    protected GameObject option;

    //나가기 팝업
    GameObject exitPopUpObj;
    GameObject[] exitButtons;

    //Comment : Exit 체크용 변수
    int exitCheck;

    int exitNum;

    //메뉴 선택하는 배열의 인덱스(현재 선택된 메뉴)
    int curMenu;

    protected bool isOption;
    protected float inputDelay = 0.15f;

    protected Coroutine menuCo;
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
        if (!option.activeSelf && !main_continue.activeSelf && !main_new.activeSelf)
            curState = CurState.main;

        // if (option.activeSelf)
        //     curState = CurState.optionDepth1;

        if (curState == CurState.main)
        {
            if (menuCo == null)
            {
                menuCo = StartCoroutine(MenuSelect());
            }
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

        menuButtons = new Button[4];
        menuButtons[0] = continueImage = GetUI<Button>("ContinueImage");
        menuButtons[1] = newImage = GetUI<Button>("NewImage");
        menuButtons[2] = optionImage = GetUI<Button>("OptionImage");
        menuButtons[3] = exitImage = GetUI<Button>("ExitImage");

        exitButtons = new GameObject[2];
        exitButtons[0] = GetUI("YesImage");
        exitButtons[1] = GetUI("NoImage");

        exitPopUpObj = GetUI("ExitPopUp");

        main_continue = GetUI("Background_continue");
        main_new = GetUI("Background_new");


        option = GetUI("Background_option");


        curMenu = 0;
        exitNum = 0;


    }

    // Comment : W/S 또는 위/아래 화살표 키를 이용해 메뉴 변경 가능 함수

    private IEnumerator MenuSelect()
    {
        float y = -Input.GetAxisRaw("Vertical");


        curMenu += (int)y;

        if (curMenu == menuButtons.Length)
        {
            curMenu = 0;
            menuButtons[menuButtons.Length - 1].gameObject.SetActive(false);
            menuButtons[curMenu].gameObject.SetActive(true);
            yield return null;
        }

        if (curMenu == -1)
        {
            curMenu = menuButtons.Length - 1;
            menuButtons[0].gameObject.SetActive(false);
            menuButtons[curMenu].gameObject.SetActive(true);
            yield return null;
        }

        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].gameObject.SetActive(false);
        }
        menuButtons[curMenu].gameObject.SetActive(true);

        if (y == 0)
            yield return null;
        else
            yield return inputDelay.GetDelay();
        menuCo = null;
    }


    //Comment : 선택한 메뉴로 진입하는 함수
    void SelectedEnter()
    {
        // Todo: 패드까지 지원 가능하게 바꿔야함
        if (Input.GetButtonDown("Interaction"))
        {
            switch (curMenu)
            {
                case 0:
                    Debug.Log("계속하기 선택_슬롯 선택 팝업 노출");
                    //Todo : 슬롯 선택 팝업 만들어야함 > 백엔드와 협업 필요할 듯
                    ContinueButton();
                    break;

                case 1:
                    Debug.Log("새로하기 선택_게임 시작 화면으로 이동");
                    //Todo : 게임 화면으로 이동 만들어야함
                    NewButton();
                    break;

                case 2:
                    Debug.Log("옵션 선택_옵션 팝업 노출");
                    OptionButton();
                    //Todo : 옵션 팝업 만들어야함
                    break;

                case 3:
                    Debug.Log("게임 종료 선택_게임 종료");
                    ExitButton();
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitPopUpObj.SetActive(true);
            Debug.Log("게임 종료 선택_게임 종료");
        }
    }

    public void ContinueButton()
    {
        main_continue.SetActive(true);
        curState = CurState._continue;
    }

    public void NewButton()
    {
        main_new.SetActive(true);
        curState = CurState._new;

    }

    public void OptionButton()
    {
        option.SetActive(true);
        curState = CurState.optionDepth1;
    }

    public void ExitButton()
    {
        exitPopUpObj.SetActive(true);
        exitCheck++;
        Debug.Log(exitCheck);
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
                if (Input.GetButtonDown("Interaction"))
                {
                    Debug.Log("종료");
                    if (exitCheck > 1)
                    {
                        ExitGame();
                    }
                    exitCheck++;
                }
                break;

            case 1:
                exitButtons[exitNum].GetComponent<Image>().color = Color.black;
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    exitButtons[exitNum].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                    exitNum = 0;
                }
                if (Input.GetButtonDown("Interaction"))
                {
                    exitCheck = 0;
                    exitPopUpObj.SetActive(false);
                }
                break;
        }
    }
    protected void ExitGame()
    {
#if UNITY_EDITOR
        //Comment : 유니티 에디터상에서 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //Comment : 빌드 상에서 종료
        Application.Quit();
#endif
    }

}
