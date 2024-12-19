using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class Option_GamePlay : Main_Option
{
    [Inject]
    SettingManager setManager;

    [SerializeField] GameObject miniMapAct;
    [SerializeField] GameObject miniMapFix;

    GameObject[] gamePlayButtons;
    int gamePlay_cur;

    bool isGamePlay = false;

    void Start()
    {

    }


    void Update()
    {
        if (depth2_cur == Depth2.gameplay)
        {
            if (menuCo == null)
            {
                menuCo = StartCoroutine(GamePlay_Select());
            }
        }
    }
    private IEnumerator GamePlay_Select()
    {//요거 수정해야함
        float y = Input.GetAxisRaw("Vertical");

        gamePlay_cur += (int)y;

        if (gamePlay_cur == gamePlayButtons.Length)
        {
            gamePlay_cur = 0;
            gamePlayButtons[gamePlayButtons.Length-1].GetComponent<TMP_Text>().color = Color.white;
            gamePlayButtons[gamePlay_cur].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
            yield return null;
        }

        if (gamePlay_cur == -1)
        {
            gamePlay_cur = gamePlayButtons.Length - 1;
            gamePlayButtons[0].GetComponent<TMP_Text>().color = Color.white;
            gamePlayButtons[gamePlay_cur].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
            yield return null;
        }

        for (int i = 0; i < gamePlayButtons.Length; i++)
        {
            gamePlayButtons[i].GetComponent<TMP_Text>().color = Color.white;
        }

        gamePlayButtons[gamePlay_cur].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);

        yield return inputDelay.GetDelay();
        menuCo = null;
    }

    private void GamePlay_SelectNoT()
    {
        float y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            gamePlayButtons[gamePlay_cur].GetComponent<TMP_Text>().color = Color.white;

            if (gamePlay_cur == gamePlayButtons.Length - 1)
            {
                gamePlay_cur = 0;
                gamePlayButtons[gamePlay_cur].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
                return;
            }

            gamePlay_cur++;
            gamePlayButtons[gamePlay_cur].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            gamePlayButtons[gamePlay_cur].GetComponent<TMP_Text>().color = Color.white;

            if (gamePlay_cur == 0)
            {
                gamePlay_cur = gamePlayButtons.Length - 1;
                gamePlayButtons[gamePlay_cur].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
                return;
            }

            gamePlay_cur--;
            gamePlayButtons[gamePlay_cur].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
        }
    }

    void Init()
    {
        miniMapAct = GetUI("");
        miniMapFix = GetUI("");
    }
}
