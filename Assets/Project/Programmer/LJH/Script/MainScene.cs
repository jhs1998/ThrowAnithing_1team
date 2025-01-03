using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : BaseUI
{

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

    //로직 변경으로 필요한 컬러
    Color curButtonColor = new(0.5f, 0.2f, 0);
    Color notButtonColor = new(0.5f, 0.2f, 0, 0.1f);

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
        if (!option.activeSelf && !main_continue.activeSelf && !main_new.activeSelf && !exitPopUpObj.activeSelf)
        {
                if (menuCo == null)
                {
                    menuCo = StartCoroutine(MenuSelect());
                }
                SelectedEnter();
        }

        if (exitPopUpObj.activeSelf)
            ExitPopUp();

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
    // Comment : 함수 내 주석처리된 코드는 비활성화/활성화 방식일 때 코드, 혹시 몰라 남겨놓겠음

    private IEnumerator MenuSelect()
    {
        float y = -InputKey.GetAxisRaw(InputKey.Vertical);


        curMenu += (int)y;

        if (curMenu == menuButtons.Length)
        {
            curMenu = 0;
            //menuButtons[menuButtons.Length - 1].gameObject.SetActive(false);
            menuButtons[menuButtons.Length - 1].GetComponent<Image>().color = notButtonColor;
            //menuButtons[curMenu].gameObject.SetActive(true);
            menuButtons[curMenu].GetComponent<Image>().color = curButtonColor;

            yield return null;
        }

        if (curMenu == -1)
        {
            curMenu = menuButtons.Length - 1;
            //menuButtons[0].gameObject.SetActive(false);
            menuButtons[0].GetComponent<Image>().color = notButtonColor;
            //menuButtons[curMenu].gameObject.SetActive(true);
            menuButtons[curMenu].GetComponent<Image>().color = curButtonColor;

            yield return null;
        }

        for (int i = 0; i < menuButtons.Length; i++)
        {
            //menuButtons[i].gameObject.SetActive(false);
            menuButtons[i].GetComponent<Image>().color = notButtonColor;
        }
        //menuButtons[curMenu].gameObject.SetActive(true);
        menuButtons[curMenu].GetComponent<Image>().color = curButtonColor;

        if (y == 0)
            yield return null;
        else
            yield return inputDelay.GetDelay();
        menuCo = null;
    }


    //Comment : 선택한 메뉴로 진입하는 함수
    void SelectedEnter()
    {
        if (InputKey.GetButtonDown(InputKey.Interaction))
        {
            switch (curMenu)
            {
                case 0:
                    ContinueButton();
                    break;

                case 1:
                    NewButton();
                    break;

                case 2:
                    OptionButton();
                    break;

                case 3:
                    ExitButton();
                    break;
            }
        }

        if (InputKey.GetButtonDown(InputKey.Cancel))
        {
            ExitButton();
        }
    }

    public void ContinueButton()
    {
        main_continue.SetActive(true);
    }

    public void NewButton()
    {
        main_new.SetActive(true);

    }

    public void OptionButton()
    {
        option.SetActive(true);
    }

    public void ExitButton()
    {
        if (exitCheck <= 0)
        {
            exitPopUpObj.SetActive(true);
            exitCheck++;
        }
    }

    void ExitPopUp()
    {
        switch (exitNum)
        {
            case 0:
                exitButtons[exitNum].GetComponent<Image>().color = Color.black;
                if (InputKey.GetButtonDown(InputKey.Horizontal))
                {
                    exitButtons[exitNum].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                    exitNum = 1;
                }
                if (InputKey.GetButtonDown(InputKey.Interaction))
                {
                    ExitYes();
                }
                break;

            case 1:
                exitButtons[exitNum].GetComponent<Image>().color = Color.black;
                if (InputKey.GetButtonDown(InputKey.Horizontal))
                {
                    exitButtons[exitNum].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                    exitNum = 0;
                }
                if (InputKey.GetButtonDown(InputKey.Interaction))
                {
                    ExitNo();
                }
                break;
        }
    }

    public void ExitYes()
    {
        if (exitCheck > 1)
        {
            ExitGame();
        }
        exitCheck++;
    }

    public void ExitNo()
    {
        exitCheck = 0;
        exitButtons[exitNum].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
        exitNum = 0;
        exitButtons[exitNum].GetComponent<Image>().color = Color.black;
        exitPopUpObj.SetActive(false);
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
