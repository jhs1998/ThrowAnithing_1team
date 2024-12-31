using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Failure : MonoBehaviour
{
    [SerializeField] GameObject scoreBoard;
    Coroutine co;


    private void OnEnable()
    {
        //다른 UI 다 닫아야함
        // .SetActive(false);
        co = StartCoroutine(GameOver());
    }

    private void OnDisable()
    {
        StopCoroutine(co);
    }


    IEnumerator GameOver()
    {
        Debug.Log("ㅁㄴㅇ");
        yield return 2f.GetRealTimeDelay();

        scoreBoard.SetActive(true);
        gameObject.SetActive(false);
    }


}
