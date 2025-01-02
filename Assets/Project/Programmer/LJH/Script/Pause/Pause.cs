using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pause : Main_Option
{
    GameObject pause;


    Button continueButton_P;
    Button optionButton_P;
    Button exitButton_P;

    [SerializeField] GameObject ingameOptions;

    int curMenu_p;

    Button[] pauseButtons;
    Button[] exitButtons_p;

    Coroutine settingCo;

    int exitNum_p;

    GameObject exitPopUpObj_p;

    //Comment : 게임 포즈되었는지 체크용 불변수
    bool isPaused;


    private void Awake()
    {
        Bind();
        Init();
    }
    private void OnEnable()
    {
        StartCoroutine(MenuSelect());
    }
    private void Update()
    {
        if (pause.activeSelf)
        {
            if (InputKey.GetButtonDown(InputKey.Interaction))
                SelectedEnter();

            //Time.timeScale = 0;

        }
        else if (!pause.activeSelf)
        {
            //Time.timeScale = 1f;

        }
    }



    private IEnumerator MenuSelect()
    {
        while (true)
        {
            float y = -InputKey.GetAxisRaw(InputKey.Vertical);
            curMenu_p += (int)y;

            //Comment : 마지막 버튼일 때, 첫 버튼으로 돌아가게
            if (curMenu_p == pauseButtons.Length)
            {
                curMenu_p = 0;
                pauseButtons[pauseButtons.Length - 1].gameObject.SetActive(false);
                pauseButtons[curMenu_p].gameObject.SetActive(true);
                yield return null;
            }

            //Comment : 첫 버튼일 때, 마지막 버튼으로 돌아가게
            if (curMenu_p == -1)
            {
                curMenu_p = pauseButtons.Length - 1;
                pauseButtons[0].gameObject.SetActive(false);
                pauseButtons[curMenu_p].gameObject.SetActive(true);
                yield return null;
            }

            //Comment : 선택하지 않은 버튼 모두 비활성화 작업
            for (int i = 0; i < pauseButtons.Length; i++)
            {
                pauseButtons[i].gameObject.SetActive(false);
            }

            //Comment : 선택한 버튼 활성화
            pauseButtons[curMenu_p].gameObject.SetActive(true);

            if (y == 0)
                yield return null;
            else
                yield return inputDelay.GetRealTimeDelay();
        }
    }


    //Comment : 선택한 메뉴로 진입하는 함수
    void SelectedEnter()
    {
        if (InputKey.GetButtonDown(InputKey.Interaction))
        {
            switch (curMenu_p)
            {
                case 0:
                    Debug.Log("게임 다시 진행");
                    ContinueButton();
                    break;

                case 1:
                    Debug.Log("옵션 선택_옵션 팝업 노출");
                    //OptionButton();
                    Panel.ChangeBundle(PausePanel.Bundle.Option);
                    break;

                case 2:
                    Debug.Log("게임 종료");
                    ExitButton();
                    break;
            }
        }
    }

    public void ContinueButton()
    {
        Panel.ChangeBundle(PausePanel.Bundle.None);
    }

    public void OptionButton()
    {
        Panel.ChangeBundle(PausePanel.Bundle.Option);
    }

    public void ExitButton()
    {
        exitPopUpObj_p.SetActive(true);
    }


    private void Init()
    {
        pause = GetUI("pause");

        pauseButtons = new Button[3];

        pauseButtons[0] = continueButton_P = GetUI<Button>("ContinueImage");
        pauseButtons[1] = optionButton_P = GetUI<Button>("OptionImage");
        pauseButtons[2] = exitButton_P = GetUI<Button>("ExitImage");

        exitPopUpObj_p = GetUI("ExitPopUp");

        exitButtons_p = new Button[2];
        exitButtons_p[0] = GetUI<Button>("Interaction");
        exitButtons_p[1] = GetUI<Button>("Negative");

    }

}
