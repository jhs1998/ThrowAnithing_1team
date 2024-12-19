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
    GameObject[] ThreeButtons;

    int gamePlay_Ver;
    int gamePlay_Ho;

    void Start()
    {

    }


    void Update()
    {
        Debug.Log(isGameplay);
        if (isGameplay)
        {
            if (menuCo == null)
            {
                menuCo = StartCoroutine(GamePlay_Select());
                menuCo = StartCoroutine(ThreeButtons_Select());
            }
        }
    }
    private IEnumerator GamePlay_Select()
    {
        float y = Input.GetAxisRaw("Vertical");

        StopCoroutine("ThreeButtons_Select");

        gamePlay_Ver += (int)y;

        if (gamePlay_Ver == gamePlayButtons.Length)
        {
            gamePlay_Ver = 0;
            gamePlayButtons[gamePlayButtons.Length-1].GetComponent<TMP_Text>().color = Color.white;
            gamePlayButtons[gamePlay_Ver].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
            yield return null;
        }

        if (gamePlay_Ver == -1)
        {
            gamePlay_Ver = gamePlayButtons.Length - 1;
            gamePlayButtons[0].GetComponent<TMP_Text>().color = Color.white;
            gamePlayButtons[gamePlay_Ver].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
            yield return null;
        }

        for (int i = 0; i < gamePlayButtons.Length; i++)
        {
            gamePlayButtons[i].GetComponent<TMP_Text>().color = Color.white;
        }

        gamePlayButtons[gamePlay_Ver].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);

        yield return inputDelay.GetDelay();
        menuCo = null;
    }

    private IEnumerator ThreeButtons_Select()
    {
        float x = Input.GetAxisRaw("Horizontal");

        StopCoroutine("GamePlay_Select");

        gamePlay_Ver += (int)x;

        if (gamePlay_Ho == ThreeButtons.Length)
        {
            gamePlay_Ho = 0;
            ThreeButtons[ThreeButtons.Length - 1].GetComponent<TMP_Text>().color = Color.white;
            ThreeButtons[gamePlay_Ho].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
            yield return null;
        }

        if (gamePlay_Ho == -1)
        {
            gamePlay_Ho = ThreeButtons.Length - 1;
            ThreeButtons[0].GetComponent<TMP_Text>().color = Color.white;
            ThreeButtons[gamePlay_Ho].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
            yield return null;
        }

        for (int i = 0; i < ThreeButtons.Length; i++)
        {
            ThreeButtons[i].GetComponent<TMP_Text>().color = Color.white;
        }

        ThreeButtons[gamePlay_Ho].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);

        yield return inputDelay.GetDelay();
        menuCo = null;
    }


    void Init()
    {
        miniMapAct = GetUI("");
        miniMapFix = GetUI("");
    }
}
