using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using Zenject;

public class Option_GamePlay : Main_Option
{
    [Inject]
    SettingManager setManager;

    [SerializeField] GameObject miniMapAct;
    [SerializeField] GameObject miniMapFix;

    GameObject[,] buttons;

    int gamePlay_Ho = 0;
    int gamePlay_Ver = 1;

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
            if (gamePlay_Ver == 3)
                gamePlay_Ver = 1;
                         
            if (gamePlay_Ver <= 0)
                gamePlay_Ver = 2;
        }


        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if(buttons[i,j] == null)
                    continue;

                buttons[i, j].GetComponent<TMP_Text>().color = Color.white;
            }
        }

        buttons[gamePlay_Ver, gamePlay_Ho].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
        yield return inputDelay.GetDelay();
        menuCo = null;


    }

    void Init()
    {
        buttons = new GameObject[3,4];

        buttons[1, 0] = miniMapAct = GetUI("Activate");
        buttons[2, 0] = miniMapFix = GetUI("Fixed");
        buttons[0, 1] = GetUI("AcceptButton_gamaplay");
        buttons[0, 2] = GetUI("CancelButton_gamaplay");
        buttons[0, 3] = GetUI("DefaultButton_gamaplay");


        gameplayOnOff = GetUI("GameplayOnOff");
    }
}
