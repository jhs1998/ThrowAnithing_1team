using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class Pause : MainScene
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
    }
    void Start()
    {
        Init();
    }

    private void Update()
    {
        if (pause.activeSelf)
        {
            if (settingCo == null)
                settingCo = StartCoroutine(MenuSelect());

            if (Input.GetButtonDown("Interaction"))
                SelectedEnter();

            //Time.timeScale = 0;
            isPaused = true;
        }
        else if (!pause.activeSelf)
        {
            Time.timeScale = 1f;
            isPaused = false;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("esc키눌렸음");
            PauseOnOff();
        }

    }

    void PauseOnOff()
    {
        pause.SetActive(!pause.activeSelf);
    }

    private IEnumerator MenuSelect()
    {
        float y = -Input.GetAxisRaw("Vertical");


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

        yield return new WaitForSecondsRealtime(inputDelay);
        settingCo = null;
    }


    //Comment : 선택한 메뉴로 진입하는 함수
    void SelectedEnter()
    {
        if (Input.GetButtonDown("Interaction"))
        {
            switch (curMenu_p)
            {
                case 0:
                    Debug.Log("게임 다시 진행");
                    ContinueButton();
                    break;

                case 1:
                    Debug.Log("옵션 선택_옵션 팝업 노출");
                    OptionButton();
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
        pause.SetActive(false);
    }

    public void OptionButton()
    {
        ingameOptions.SetActive(true);
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
