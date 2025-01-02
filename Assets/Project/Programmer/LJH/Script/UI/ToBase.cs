using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBase : MonoBehaviour
{
    public static void ToLobby()
    {
        LoadingToBase.LoadScene(SceneName.LobbyScene);
    }
}
