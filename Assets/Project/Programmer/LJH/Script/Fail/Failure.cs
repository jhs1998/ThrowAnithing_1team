using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Failure : MonoBehaviour
{
    [SerializeField] GameObject result;
    Coroutine co;


    private void OnEnable()
    {
        //�ٸ� UI �� �ݾƾ���
        // .SetActive(false);
        co = StartCoroutine(GameOver());

    }

    private void OnDisable()
    {
        StopCoroutine(co);
    }


    IEnumerator GameOver()
    {
        yield return 2f.GetRealTimeDelay();

        result.SetActive(true);
        gameObject.SetActive(false);
    }


}
