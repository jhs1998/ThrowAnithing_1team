using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingEnter : MonoBehaviour
{
    void EnterStage()
    {
        //�� ĳ���ؾ���
        SceneManager.LoadScene(SceneName.ToStageLoading);
    }
}
