using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPopup : MonoBehaviour
{
    [SerializeField] GameObject pause;
    private void Update()
    {
        ExitPopUp();
    }
    void ExitPopUp()
    {

        if (Input.GetButtonDown("Interaction"))
            SceneManager.LoadScene("LobbyScene");

        if (Input.GetButtonDown("Negative"))
        {
            Debug.Log("C´­·¶À½");
            Invoke("Exit", 0.1f);
            pause.SetActive(false);
        }
    }

    private void Exit()
    {
        gameObject.SetActive(false);
    }
}
