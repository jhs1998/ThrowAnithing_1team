using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Depth2
{
    gameplay,
    language,
    sound,
    notDepth2 = 4
}

public class Main_Option : MainScene
{
    //옵션 1Depth
    GameObject[] depth1;

    GameObject gamePlay;
    GameObject language;
    GameObject sound;
    GameObject input;
    GameObject exit;

    //옵션 Depth2 바인딩
    GameObject gameplayPannel;
    GameObject languagePannel;
    GameObject soundPannel;
    GameObject inputPannel;

    protected int depth1_cur;

    //옵션 Depth2 체크용 변수
    protected Depth2 depth2_cur = Depth2.notDepth2;

    protected GameObject gameplayOnOff;
    protected GameObject languageOnOff;
    protected GameObject soundOnOff;

    void Start()
    {
        Init();

        
    }


    private void Update()
    {
        if (gameplayOnOff.activeSelf)
            return;
        if (languageOnOff.activeSelf)
            return;
        if (soundOnOff.activeSelf)
            return;

        if (gameObject.activeSelf)
        {
            OptionTitle();
            if (menuCo == null)
            {
                menuCo = StartCoroutine(Depth1_Select());
            }
            SelectedEnter();
        }
    }

    void OptionTitle()
    {
        GameObject optionTitle = GetUI("optionTitle");

        switch (depth1_cur)
        {
            case 0:
                optionTitle.GetComponent<TMP_Text>().text = "MiniMap";
                break;

            case 1:
                optionTitle.GetComponent<TMP_Text>().text = "Language";
                break;

            case 2:
                optionTitle.GetComponent<TMP_Text>().text = "Sound";
                break;

            case 3:
                optionTitle.GetComponent<TMP_Text>().text = "Input";
                break;

            case 4:
                optionTitle.GetComponent<TMP_Text>().text = "";
                break;
        }

        OptionD2Show();

    }

    void OptionD2Show()
    {
        if (depth1_cur == 0)
            gameplayPannel.SetActive(true);
        else
            gameplayPannel.SetActive(false);

        if (depth1_cur == 1)
            languagePannel.SetActive(true);
        else
            languagePannel.SetActive(false);

        if (depth1_cur == 2)
            soundPannel.SetActive(true);
        else
            soundPannel.SetActive(false);

        if (depth1_cur == 3)
            inputPannel.SetActive(true);
        else
            inputPannel.SetActive(false);


    }
    private IEnumerator Depth1_Select()
    {
        float y = -Input.GetAxisRaw("Vertical");


        depth1_cur += (int)y;

        if (depth1_cur == depth1.Length)
        {
            depth1_cur = 0;
            depth1[depth1.Length-1].GetComponent<TMP_Text>().color = Color.white;
            depth1[depth1_cur].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
            yield return null;
        }

        if (depth1_cur == -1)
        {
            depth1_cur = depth1.Length - 1;
            depth1[0].GetComponent<TMP_Text>().color = Color.white;
            depth1[depth1_cur].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
            yield return null;
        }

        for (int i = 0; i < depth1.Length; i++)
        {
            depth1[i].GetComponent<TMP_Text>().color = Color.white;
        }
        depth1[depth1_cur].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);

        yield return inputDelay.GetDelay();
        menuCo = null;
    }

    void SelectedEnter()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (depth1_cur)
            {
                case 0:
                    Debug.Log("게임플레이 선택");
                    gameplayOnOff.SetActive(true);
                    break;

                case 1:
                    Debug.Log("언어 선택");
                    languageOnOff.SetActive(true);
                    break;

                case 2:
                    Debug.Log("소리 선택");
                    soundOnOff.SetActive(true);
                    break;

                case 3:
                    Debug.Log("조작 키 설명 이미지 노출");
                    //Todo : 조작키 설명
                    break;

                case 4:
                    Debug.Log("욥션 화면 나가기");
                    depth2Checker(depth1_cur);
                    gameObject.SetActive(false);
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            Debug.Log("옵션 화면 나가기");
        }
    }

    Depth2 depth2Checker(int menuNum)
    {
        return (Depth2)menuNum;
    }


    private void Init()
    {
        depth1 = new GameObject[5];

        depth1[0] = gamePlay = GetUI("GamePlay");
        depth1[1] = language = GetUI("Language");
        depth1[2] = sound = GetUI("Sound");
        depth1[3] = input = GetUI("Input");
        depth1[4] = exit = GetUI("Update");

        gameplayPannel = GetUI("GamePlayPackage");
        languagePannel = GetUI("LanguagePackage");
        soundPannel = GetUI("SoundPackage");
        inputPannel = GetUI("InputImage");

        gameplayOnOff = GetUI("GameplayOnOff");
        languageOnOff = GetUI("LanguageOnOff");
        soundOnOff = GetUI("SoundOnOff");
        depth1_cur = 0;

    }
}
