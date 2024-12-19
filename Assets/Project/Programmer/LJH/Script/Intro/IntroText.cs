using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroText : Intro
{
    TMP_Text myText;

    private void OnEnable()
    {
        for (int i = 0; i < textArray.Length; i++)
        {
            //Comment 해당 스크립트가 textArary[i] 와 같으면 myText 변수에 textArray[i]를 할당
            if (this == textArray[i])
                myText = textArray[i].GetComponent<TMP_Text>();
        }


        textStorage = myText.text;
        myText.text = " ";
        StartCoroutine(TextWriteCo(myText, textStorage));
    }

}
