using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class Pause : MainScene
{
    GameObject pause;


    GameObject continueButton;
    GameObject optionButton;
    GameObject exitButton;

    [SerializeField] GameObject ingameOptions;

    int curMenu;

    GameObject[] pauseButtons;
    GameObject[] exitButtons;

    Coroutine settingCo;

    int exitNum;

    GameObject exitPopUpObj;
    GameObject exitNo;



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


        curMenu += (int)y;

        if (curMenu == pauseButtons.Length)
        {
            curMenu = 0;
            pauseButtons[pauseButtons.Length - 1].SetActive(false);
            pauseButtons[curMenu].SetActive(true);
            yield return null;
        }

        if (curMenu == -1)
        {
            curMenu = pauseButtons.Length - 1;
            pauseButtons[0].SetActive(false);
            pauseButtons[curMenu].SetActive(true);
            yield return null;
        }

        for (int i = 0; i < pauseButtons.Length; i++)
        {
            pauseButtons[i].SetActive(false);
        }
        pauseButtons[curMenu].SetActive(true);

        yield return inputDelay.GetDelay();
        settingCo = null;
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
                    Debug.Log("게임 다시 진행");
                    pause.SetActive(false);
                    break;

                case 1:
                    Debug.Log("옵션 선택_옵션 팝업 노출");
                    ingameOptions.SetActive(true);
                    break;

                case 2:
                    Debug.Log("게임 종료 선택_게임 종료");
                    pause.SetActive(false);
                    break;
            }
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
                if (Input.GetButtonDown("Interaction"))
                    ExitGame();
                break;

            case 1:
                exitNo.GetComponent<Image>().color = Color.black;
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    exitNo.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                    exitNum = 0;
                }
                if (Input.GetButtonDown("Interaction"))
                    exitPopUpObj.SetActive(false);
                break;

        }
    }
    void ExitGame()
    {
#if UNITY_EDITOR
        //Comment : 유니티 에디터상에서 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //Comment : 빌드 상에서 종료
        Application.Quit();
#endif
    }

    private void Init()
    {
        pause = GetUI("pause");

        pauseButtons = new GameObject[3];

        pauseButtons[0] = continueButton = GetUI("ContinueImage");
        pauseButtons[1] = optionButton = GetUI("OptionImage");
        pauseButtons[2] = exitButton = GetUI("ExitImage");

    }

}
