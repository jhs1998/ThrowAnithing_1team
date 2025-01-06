using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeText : MonoBehaviour
{
    Color backCol;
    Color textCol;
    [SerializeField] Canvas canvas;

    Coroutine textCo;

    private void OnEnable()
    {
        // 임시로 EditorOnly 태그 박아넣음
        canvas = GameObject.FindWithTag(Tag.EditorOnly).GetComponent<Canvas>();
        gameObject.transform.SetParent(canvas.transform);
        backCol = gameObject.GetComponent<Image>().color;
        textCol = gameObject.GetComponentInChildren<TMP_Text>().color;
    }

    private void OnDisable()
    {
        StopCoroutine(textCo);
        textCo = null;
    }

    private void Update()
    {
        TextHide();
        if(textCo == null)
        textCo = StartCoroutine(TextDelete());
    }

    void TextHide()
    {
        backCol.a -= 0.5f * Time.deltaTime;
        textCol.a -= 0.5f * Time.deltaTime;

        gameObject.GetComponent<Image>().color = backCol;
        gameObject.GetComponentInChildren<TMP_Text>().color = textCol;
    }

    IEnumerator TextDelete()
    {
        yield return 1f.GetRealTimeDelay();
        
        // 테스트 코드
        //gameObject.SetActive(false);
        
        Destroy(gameObject);
    }
}
