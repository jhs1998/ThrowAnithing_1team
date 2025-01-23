using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroText : Intro
{
    TMP_Text myText;

    //Comment �ݺ��� ������ �� ���� ��ũ��Ʈ �ѱ�� ����
    int count;

    private void OnEnable()
    {

        for (count = 0; count < textArray.Length; count++)
        {
            //Comment �ش� ��ũ��Ʈ�� textArary[i] �� ������ myText ������ textArray[i]�� �Ҵ�
            if (this == textArray[count])
                myText = textArray[count].GetComponent<TMP_Text>();
        }

        // Comment : �ؽ�Ʈ���丮���� �ؽ�Ʈ ������ ������ �� �ڷ�ƾ�� ����� �ؽ�Ʈ���丮���� �ؽ�Ʈ�� �ѱ��ھ� ���
        textStorage = myText.text;
        myText.text = " ";
        StartCoroutine(TextWriteCo(myText, textStorage));
        
    }

    private void OnDisable()
    {
        //Comment : textArray[connt]�� �� ��ũ��Ʈ�� ������ ���� �ε��� Ȱ��ȭ 
        for (count = 0; count < textArray.Length; count++)
        {
            if (this == textArray[count])
            {
                if (count == textArray.Length - 1)
                {
                    //Todo: ���� ���
                    SceneManager.LoadScene("TitleScene");
                    return;
                }
                count++;
                textArray[count].gameObject.SetActive(true);
            }
        }
    }

    protected override void TextStart()
    {
        return;
    }
    protected override void NarrChange()
    {
        return;
    }

}
