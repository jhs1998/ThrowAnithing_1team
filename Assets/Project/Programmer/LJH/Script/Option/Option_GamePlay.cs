using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Option_GamePlay : Main_Option
{
    SettingManager setManager;

    [SerializeField] GameObject miniMapAct;
    [SerializeField] GameObject miniMapFix;
    [SerializeField] GameObject languageDrop;

    GameObject actChecked;
    GameObject fixChecked;

    protected GameObject[,] buttons;

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


        if (gamePlay_Ver != 0)
        {
            if (x != 0)
            {
                gamePlay_Ver = 0;
                gamePlay_Ho = 1;
            }
        }

        if (gamePlay_Ho != 0)
        {
            if (y != 0)
            {
                gamePlay_Ho = 0;
                gamePlay_Ver = 1;
            }
        }

        if (gamePlay_Ver <= 0)
        {
            if (gamePlay_Ho == 4)
                gamePlay_Ho = 1;

            if (gamePlay_Ho <= 0)
                gamePlay_Ho = 3;
        }

        if (gamePlay_Ho <= 0)
        {
            if (gamePlay_Ver == 4)
                gamePlay_Ver = 1;

            if (gamePlay_Ver <= 0)
                gamePlay_Ver = 3;
        }


        ButtonReset();

        buttons[gamePlay_Ver, gamePlay_Ho].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
        if (x == 0 && y == 0)
            yield return null;
        else
            yield return inputDelay.GetRealTimeDelay();
        menuCo = null;


    }

    void ButtonReset()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (buttons[i, j] == null)
                    continue;

                buttons[i, j].GetComponent<TMP_Text>().color = Color.white;
            }
        }
    }

    void ButtonSelect()
    {
        switch (gamePlay_Ver, gamePlay_Ho)
        {
            case (0, 1):
                acceptButton.onClick.Invoke();
                break;

            case (0, 2):
                cancelButton.onClick.Invoke();
                break;

            case (0, 3):
                defaultButton.onClick.Invoke();
                break;

            case (1, 0):
                ActCheck();
                break;

            case (2, 0):
                FixCheck();
                break;

            case (3, 0):
                Debug.Log("랭귀지");
                break;

        }
    }


    //Todo : Depth2 일때만 처리되게 해야함
    public void ActCheck()
    {
        actChecked.SetActive(!actChecked.activeSelf);
    }

    public void FixCheck()
    {
        fixChecked.SetActive(!fixChecked.activeSelf);
    }
    public void AcceptButton()
    {
        MinimapCheck();
        actChecked.SetActive(newAct);
        fixChecked.SetActive(newFix);

        preAct = actChecked.activeSelf;
        preFix = fixChecked.activeSelf;

        ButtonReset();

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

        ButtonReset();

        gameplayOnOff.SetActive(false);
        //Todo : depth1으로 복귀
    }

    public void DefaultButton()
    {
        // defaultPopUp.SetActive(true);
        // Todo: 팝업 과정 거쳐야함


        actChecked.SetActive(defaultAct);
        fixChecked.SetActive(defaultFix);

        preAct = actChecked.activeSelf;
        preFix = fixChecked.activeSelf;

        ButtonReset();

        gameplayOnOff.SetActive(false);

        //Todo : depth1으로 복귀
    }

    void Init()
    {
        buttons = new GameObject[4, 4];

        buttons[1, 0] = miniMapAct = GetUI("Activate");
        buttons[2, 0] = miniMapFix = GetUI("Fixed");
        buttons[3, 0] = languageDrop = GetUI("Language");
        buttons[0, 1] = GetUI("AcceptButton_gameplay");
        buttons[0, 2] = GetUI("CancelButton_gameplay");
        buttons[0, 3] = GetUI("DefaultButton_gameplay");


        acceptButton = GetUI<Button>("AcceptButton_gameplay");

        defaultButton = GetUI<Button>("DefaultButton_gameplay");
        cancelButton = GetUI<Button>("CancelButton_gameplay");
        actChecked = GetUI("MiniMapActChecked");
        fixChecked = GetUI("MiniMapFixChecked");

        gameplayOnOff = GetUI("GameplayOnOff");

        defaultAct = true;
        defaultFix = true;

        preAct = actChecked.activeSelf;
        preFix = fixChecked.activeSelf;
    }
}
