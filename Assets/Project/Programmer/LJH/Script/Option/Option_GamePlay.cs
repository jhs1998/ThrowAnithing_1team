using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

public class Option_GamePlay : Main_Option
{
    [Inject]
    SettingManager setManager;

    [SerializeField] GameObject miniMapAct;
    [SerializeField] GameObject miniMapFix;
    [SerializeField] GameObject languageDrop;

    GameObject actChecked;
    GameObject fixChecked;

    GameObject[,] buttons;

    int gamePlay_Ho = 0;
    int gamePlay_Ver = 1;

    GameObject defaultPopUp;


    bool preAct;
    bool newAct;
    bool defaultAct;

    bool preFix;
    bool newFix;
    bool defaultFix;

    [SerializeField] Button acceptButton;
    [SerializeField] Button cancelButton;
    [SerializeField] Button defaultButton;


    
    void Start()
    {
        Init();
    }


    void Update()
    {
        Debug.Log(preAct);
        Debug.Log(preFix);
        
        if (gameplayOnOff.activeSelf)
        {
            if (menuCo == null)
            {
                menuCo = StartCoroutine(GamePlay_Select());
            }

            if (Input.GetButtonDown("Interaction"))
                ButtonSelect();
        }

    }
    private IEnumerator GamePlay_Select()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = -Input.GetAxisRaw("Vertical");
        

        gamePlay_Ho += (int)x;
        gamePlay_Ver += (int)y;


        if(gamePlay_Ver != 0)
        {
            if (x != 0)
            {
                gamePlay_Ver = 0;
                gamePlay_Ho = 1;
            }
        }

        if(gamePlay_Ho !=0)
        {
            if (y != 0)
            {
                gamePlay_Ho = 0;
                gamePlay_Ver = 1;
            }
        }

        if(gamePlay_Ver <= 0)
        {
            if (gamePlay_Ho == 4)
                gamePlay_Ho = 1;

            if (gamePlay_Ho <= 0)
                gamePlay_Ho= 3;
        }

        if (gamePlay_Ho <= 0)
        {
            if (gamePlay_Ver == 4)
                gamePlay_Ver = 1;
                         
            if (gamePlay_Ver <= 0)
                gamePlay_Ver = 3;
        }


        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if(buttons[i,j] == null)
                    continue;

                buttons[i, j].GetComponent<TMP_Text>().color = Color.white;
            }
        }

        Debug.Log($"gamePlay_Ver${gamePlay_Ver} gamePlay_Ho{gamePlay_Ho}");
        buttons[gamePlay_Ver, gamePlay_Ho].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
        yield return inputDelay.GetDelay();
        menuCo = null;


    }

    void ButtonSelect()
    {
        Debug.Log("버튼선택됨");
        switch(gamePlay_Ver, gamePlay_Ho)
        {
            case (0, 1):
                Debug.Log("0 1 실행");
                AcceptButton();
                break;

            case (0, 2):
                CancelButton();
                break;

            case (0, 3):
                DefaultButton();
                break;

            case (1, 0):
                Debug.Log("1 0 실행");
                break;

            case (2, 0):
                break;

            case (3, 0):
                break;

        }
    }

    //Todo : Depth2 일때만 처리되게 해야함
    public void ActCheck()
    {
        Debug.Log("액트체크실행됨");
        actChecked.SetActive(!actChecked.activeSelf);
    }

    public void FixCheck()
    {
        Debug.Log("픽스체크실행됨");
        fixChecked.SetActive(!fixChecked.activeSelf);
    }
    public void AcceptButton()
    {
        MinimapCheck();
        actChecked.SetActive(newAct);
        fixChecked.SetActive(newFix);

        preAct = actChecked.activeSelf;
        preFix = fixChecked.activeSelf;

        gameplayOnOff.SetActive(false);

        //Todo : depth1으로 복귀
    }

    void MinimapCheck()
    {
        newAct = actChecked.activeSelf;
        newFix = fixChecked.activeSelf;
    }

    public void CancelButton()
    {
        actChecked.SetActive(preAct);
        fixChecked.SetActive(preFix);

        gameplayOnOff.SetActive(false);
        //Todo : depth1으로 복귀
    }

    public void DefaultButton()
    {
        // defaultPopUp.SetActive(true);
        // Todo: 팝업 과정 거쳐야함


        actChecked.SetActive(defaultAct);
        fixChecked.SetActive(defaultFix);

        gameplayOnOff.SetActive(false);

        //Todo : depth1으로 복귀
    }

    void Init()
    {
        buttons = new GameObject[4,4];

        buttons[1, 0] = miniMapAct = GetUI("Activate");
        buttons[2, 0] = miniMapFix = GetUI("Fixed");
        buttons[3, 0] = languageDrop = GetUI("Language");
        buttons[0, 1] = GetUI("AcceptButton_gameplay");
        buttons[0, 2] = GetUI("CancelButton_gameplay");
        buttons[0, 3] = GetUI("DefaultButton_gameplay");

        actChecked = GetUI("MiniMapActChecked");
        fixChecked = GetUI("MiniMapFixChecked");

        gameplayOnOff = GetUI("GameplayOnOff");

        defaultAct = true;
        defaultFix = true;

        preAct = actChecked.activeSelf;
        preFix = fixChecked.activeSelf;
    }
}
