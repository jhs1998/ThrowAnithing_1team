using System.Collections;
using System.Collections.Generic;
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

    List<Button> gamePlayButtons = new List<Button>();

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

    public int _curIndex;


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

            if (InputKey.GetButtonDown(InputKey.Interaction))
                ButtonSelect();

            if (InputKey.GetButtonDown(InputKey.Interaction))
            {
                if (gamePlayButtons[_curIndex] == GetUI("CancelButton_sound"))
                {
                    CancelButton();
                }
                else if (gamePlayButtons[_curIndex] == GetUI("AcceptButton_sound"))
                {
                    AcceptButton();
                }
                else if (gamePlayButtons[_curIndex] == GetUI("DefaultButton_sound"))
                {
                    DefaultButton();
                }
                //sound_Ver = 0;
                //sound_Ho = 1;
            }
        }
        else
        {
            _curIndex = 0;
        }

    }

    private void OnDisable()
    {
        menuCo = null;
    }

    private IEnumerator GamePlay_Select()
    {
        float x = InputKey.GetAxisRaw(InputKey.Horizontal);
        float y = InputKey.GetAxisRaw(InputKey.Vertical);

        Button curButton = gamePlayButtons[_curIndex];
        for (int i = 0; i < gamePlayButtons.Count; i++)
        {
            if (gamePlayButtons[i] == curButton)
            {
                Debug.Log(curButton);
                gamePlayButtons[i].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
            }
            else
            {
                gamePlayButtons[i].GetComponent<TMP_Text>().color = Color.white;
            }
        }
        // 상단 버튼일 때
        if (_curIndex > -1 && _curIndex < 3)
        {
            // 아래 버튼 눌렀을 때
            if (y < 0)
            {
                if (_curIndex == 2)
                {
                    _curIndex = 3;
                }
                else
                {
                    _curIndex++;
                }
                

            }
            // 위 버튼 눌렀을 때
            else if (y > 0)
            {
                _curIndex--;
                if (_curIndex < 0)
                {
                    _curIndex = gamePlayButtons.Count - 1;
                }
            }
            
            
        }

        if (_curIndex > 2 && _curIndex < gamePlayButtons.Count)
        {
            if (y > 0)
            {
                _curIndex = 2;
            }
            // 오른쪽 키 눌렀을 때
            if (x > 0)
            {
                _curIndex++;
                if (_curIndex > gamePlayButtons.Count - 1)
                {
                    _curIndex = 0;
                }
            }
            else if (x < 0)
            {
                _curIndex--;
            }
        }

        // 입력없으면 프레임마다
        if (x == 0 && y == 0)
        {
            yield return null;
        }
        else
        {
            yield return inputDelay.GetRealTimeDelay();
        }
        menuCo = null;

    }

    void ButtonReset()
    {
        for (int i = 0; i < gamePlayButtons.Count - 1; i++)
        {
            if (gamePlayButtons[i] == null)
                continue;

            gamePlayButtons[i].GetComponent<TMP_Text>().color = Color.white;
        }
    }

    void ButtonSelect()
    {
        switch (_curIndex)
        {
            case 0:
                ActCheck();
                break;

            case 1:
                FixCheck();
                break;

            case 2:
                Debug.Log("랭귀지");
                break;

            case 3:
                acceptButton.onClick.Invoke();
                break;

            case 4:
                cancelButton.onClick.Invoke();
                break;

            case 5:
                defaultButton.onClick.Invoke();
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
        // buttons = new GameObject[4, 4];
        //
        // buttons[1, 0] = miniMapAct = GetUI("Activate");
        // buttons[2, 0] = miniMapFix = GetUI("Fixed");
        // buttons[3, 0] = languageDrop = GetUI("Language");
        // buttons[0, 1] = GetUI("AcceptButton_gameplay");
        // buttons[0, 2] = GetUI("CancelButton_gameplay");
        // buttons[0, 3] = GetUI("DefaultButton_gameplay");

        gamePlayButtons.Add(GetUI<Button>("Activate"));
        gamePlayButtons.Add(GetUI<Button>("Fixed"));
        gamePlayButtons.Add(GetUI<Button>("Language"));
        gamePlayButtons.Add(GetUI<Button>("AcceptButton_gameplay"));
        gamePlayButtons.Add(GetUI<Button>("CancelButton_gameplay"));
        gamePlayButtons.Add(GetUI<Button>("DefaultButton_gameplay"));

        Debug.Log($"게임플레이 버튼 드가있나{gamePlayButtons[0].name}");



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
