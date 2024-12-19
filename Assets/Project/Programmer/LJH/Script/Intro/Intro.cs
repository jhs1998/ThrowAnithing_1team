using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    protected string textStorage;


    [SerializeField] protected IntroText[] textArray;

    protected Coroutine textCo;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("TitleScene");
    }
    protected IEnumerator TextWriteCo(TMP_Text textBox, string textStorage)
    {
        int i = 0;
        while(i < textStorage.Length-1)
        {
            textBox.text += textStorage[i].ToString();

            i++;

            yield return new WaitForSeconds(0.1f);
        }

    }
}
