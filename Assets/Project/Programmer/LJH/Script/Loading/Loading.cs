using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public static string nextScene;

    [SerializeField] Slider[] progressBars;

    //Todo : 추후에 라운드 따오는 방식으로 변경
    [SerializeField] int round;

    private void Start()
    {
        round = Round.instance.curRound;
        StartCoroutine(LoadScene(round));
        Debug.Log("로딩중임");
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    //Comment : round를 따와서 라운드에 해당하는 바 컨트롤
    IEnumerator LoadScene(int round)
    {

        // 로비가 0 
        // 1라 1 
        // 2라 2
        // 보스 3

        //Comment : 이전 라운드의 바 value 채워놓는 용도
        for (int i = 1; i < round; i++)
                progressBars[i - 1].value = 100;
        

        //Comment 인덱스와 라운드 맞춰주기 위한
        round = round - 1;

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
                progressBars[round].value = Mathf.Lerp(progressBars[round].value, op.progress, timer);
                if (progressBars[round].value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBars[round].value = Mathf.Lerp(progressBars[round].value, 1f, timer);
                if (progressBars[round].value == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }


    
}
