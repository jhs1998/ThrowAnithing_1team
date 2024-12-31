using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingToBase : MonoBehaviour
{
    public static string nextScene;

    [SerializeField] Slider progressBar;

    //Todo : 추후에 라운드 따오는 방식으로 변경
    [SerializeField] int round;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        nextScene = sceneName;
        SceneManager.LoadScene("BaseLoadingScene");
    }

    //Comment : round를 따와서 라운드에 해당하는 바 컨트롤
    IEnumerator LoadScene()
    {

        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.value = Mathf.Lerp(progressBar.value, op.progress, timer);
                if (progressBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer);
                if (progressBar.value == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }



}
