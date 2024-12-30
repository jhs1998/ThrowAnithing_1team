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

    private void Start()
    {
        buttons[0].onClick.AddListener(ToBase.ToLobby);
        buttons[1].onClick.AddListener(Exit);
    }
    private void Update()
    {
        if(Input.GetButtonDown("Interaction"))
            buttons[0].onClick.Invoke();

        if(Input.GetButtonDown("Negative"))
            buttons[1].onClick.Invoke();
    }
    

    public void Exit()
    {
        gameObject.SetActive(false);
    }
}
