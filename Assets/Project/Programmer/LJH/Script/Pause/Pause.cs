using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pause : Main_Option
{
    GameObject pause;


    Button continueButton_P;
    Button optionButton_P;
    Button exitButton_P;


    int curMenu_p;

    Button[] pauseButtons;
    Button[] exitButtons_p;

    Coroutine settingCo;

    int exitNum_p;

    GameObject exitPopUpObj_p;


    //로직 변경으로 필요한 컬러
    Color _curButtonColor = new(0.5f, 0.2f, 0);
    Color _notButtonColor = new(0.5f, 0.2f, 0, 0.1f);



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
                //pauseButtons[pauseButtons.Length - 1].gameObject.SetActive(false);
                pauseButtons[pauseButtons.Length - 1].GetComponent<Image>().color = _notButtonColor;
                //pauseButtons[curMenu_p].gameObject.SetActive(true);
                pauseButtons[curMenu_p].GetComponent<Image>().color = _curButtonColor;

                yield return null;
            }

            //Comment : 첫 버튼일 때, 마지막 버튼으로 돌아가게
            if (curMenu_p == -1)
            {
                curMenu_p = pauseButtons.Length - 1;
                //pauseButtons[0].gameObject.SetActive(false);
                pauseButtons[0].GetComponent<Image>().color = _notButtonColor;
                //pauseButtons[curMenu_p].gameObject.SetActive(true);
                pauseButtons[curMenu_p].GetComponent<Image>().color = _curButtonColor;

                yield return null;
            }

            //Comment : 선택하지 않은 버튼 모두 비활성화 작업
            for (int i = 0; i < pauseButtons.Length; i++)
            {
                //pauseButtons[i].gameObject.SetActive(false);
                pauseButtons[i].GetComponent<Image>().color = _notButtonColor;
            }

            //Comment : 선택한 버튼 활성화
            //pauseButtons[curMenu_p].gameObject.SetActive(true);
            pauseButtons[curMenu_p].GetComponent<Image>().color = _curButtonColor;

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
                    _ContinueButton();
                    break;

                case 1:
                    Debug.Log("옵션 선택_옵션 팝업 노출");
                    //OptionButton();
                    Panel.ChangeBundle(PausePanel.Bundle.Option);
                    break;

                case 2:
                    Debug.Log("게임 종료");
                    _ExitButton();
                    break;
            }
        }
    }

    public void _ContinueButton()
    {
        Panel.ChangeBundle(PausePanel.Bundle.None);
    }

    public void _OptionButton()
    {
        Panel.ChangeBundle(PausePanel.Bundle.Option);
    }

    public void _ExitButton()
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

        pauseButtons[0].gameObject.SetActive(true);
        pauseButtons[1].gameObject.SetActive(true);
        pauseButtons[2].gameObject.SetActive(true);

        exitPopUpObj_p = GetUI("ExitPopUp");

        exitButtons_p = new Button[2];
        exitButtons_p[0] = GetUI<Button>("Interaction");
        exitButtons_p[1] = GetUI<Button>("Negative");

    }

}
