using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    Coroutine loadingRoutine;
    [SerializeField] Slider loadingBar;
    [SerializeField] Button loadingButton;

    public void ChangeScene(int SceneNum)
    {

        if (loadingRoutine != null)
            return;

        loadingRoutine = StartCoroutine(LoadingRoutine(SceneNum));
    }


    IEnumerator LoadingRoutine(int SceneNum)
    {
        AsyncOperation oper = SceneManager.LoadSceneAsync(SceneNum);

        oper.allowSceneActivation = false;


        while (oper.isDone == false)
        {

            if (oper.progress < 0.9f)
            {
                //로딩중
                Debug.Log($"loading = {oper.progress}");
                loadingBar.value = oper.progress;
            }
            else
            {
                //로딩 완료
                //Debug.Log("loading success");
                //oper.allowSceneActivation = true;
                break;
            }
            yield return null;
        }


        //페이크 로딩 (억지로 로딩바 채우는 눈속임버전)
        float time = 0f;
        while (time < 5f)
        {
            time += Time.deltaTime;
            loadingBar.value = time / 5f;
            yield return null;
        }
        Debug.Log("loading success");
        while (time >= 5f)
        {
            loadingButton.gameObject.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                oper.allowSceneActivation = true;

            }

            yield return null;
        }

    }








}
