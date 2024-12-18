using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    int depth1_cur;

    void Start()
    {
        Init();
    }


    private void Update()
    {
        if (curState == CurState.optionDepth1)
            return;

        OptionTitle();
        Depth1_Select();
        SelectedEnter();

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

    private void Depth1_Select()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            depth1[depth1_cur].GetComponent<TMP_Text>().color = Color.white;

            if (depth1_cur == depth1.Length - 1)
            {
                depth1_cur = 0;
                depth1[depth1_cur].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
                return;
            }

            depth1_cur++;
            depth1[depth1_cur].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            depth1[depth1_cur].GetComponent<TMP_Text>().color = Color.white;

            if (depth1_cur == 0)
            {
                depth1_cur = depth1.Length - 1;
                depth1[depth1_cur].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
                return;
            }

            depth1_cur--;
            depth1[depth1_cur].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
        }
    }

    void SelectedEnter()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (depth1_cur)
            {
                case 0:
                    Debug.Log("게임플레이 선택");
                    curState = CurState.optionDepth2;
                    break;

                case 1:
                    Debug.Log("언어 선택");
                    break;

                case 2:
                    Debug.Log("소리 선택");
                    break;

                case 3:
                    Debug.Log("조작 키 설명 이미지 노출");
                    //Todo : 조작키 설명
                    break;

                case 4:
                    Debug.Log("욥션 화면 나가기");
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


    private void Init()
    {
        depth1 = new GameObject[5];

        depth1[0] = gamePlay = GetUI("GamePlay");
        depth1[1] = language = GetUI("Language");
        depth1[2] = sound = GetUI("Sound");
        depth1[3] = input = GetUI("Input");
        depth1[4] = exit = GetUI("Exit");

        gameplayPannel = GetUI("GamePlayPackage");
        languagePannel = GetUI("LanguagePackage");
        soundPannel = GetUI("SoundPackage");
        inputPannel = GetUI("InputImage");

        depth1_cur = 0;
    }
}
