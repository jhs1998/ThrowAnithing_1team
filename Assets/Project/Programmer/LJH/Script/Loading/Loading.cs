using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public static string nextScene;

    [SerializeField] Slider[] progressBars;

    //Todo : ���Ŀ� ���� ������ ������� ����
    [SerializeField] int round;

    private void Start()
    {
        round = Round.instance.curRound;
        StartCoroutine(LoadScene(round));
        Debug.Log("�ε�����");
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene(SceneName.ToStageLoading);
    }

    //Comment : round�� ���ͼ� ���忡 �ش��ϴ� �� ��Ʈ��
    IEnumerator LoadScene(int round)
    {

        // �κ� 0 
        // 1�� 1 
        // 2�� 2
        // ���� 3


        // 0�϶� �����ְ� 1�϶� 1���ְ� 2�϶� 2���־����

        //round �� 0�϶� �������ϰ�
        //round �� 1�϶� [0] �����ϰ�
        //round �� 2�϶� [0] [1] ������



        //Comment : ���� ������ �� value ä������ �뵵
        for (int i = 0; i <= round; i++)
        {
            if(i > 0)
            progressBars[i - 1].value = 100;
        }

        //Comment �ε����� ���� �����ֱ� ����
        //round = round - 1;

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
