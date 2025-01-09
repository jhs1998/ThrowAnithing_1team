using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitPopup : MonoBehaviour
{
    [SerializeField] GameObject pause;
    [SerializeField] Button[] buttons;

    [SerializeField] SceneField mainScene;
    [SerializeField] SceneField lobbyScene;
    private void Start()
    {
        buttons[0].onClick.AddListener(ExitScene);
        buttons[1].onClick.AddListener(Exit);
    }
    private void Update()
    {
        if(InputKey.GetButtonDown(InputKey.PrevInteraction))
            buttons[0].onClick.Invoke();

        if(InputKey.GetButtonDown(InputKey.PopUpClose))
            buttons[1].onClick.Invoke();
    }
    
    private void ExitScene()
    {
        // ·Îºñ¾À¿¡¼­´Â ¸ÞÀÎ¾ÀÀ¸·Î º¸³¿
        if (SceneManager.GetActiveScene().name == lobbyScene.SceneName)
        {
            LoadingToBase.LoadScene(mainScene);
        }
        else
        {
            LoadingToBase.LoadScene(lobbyScene);
        }
    }


    public void Exit()
    {
        gameObject.SetActive(false);
    }
}
