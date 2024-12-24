using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Status : MonoBehaviour
{
    //Comment : 체력,마나,스테미나 중 1개 / 해당 수치의 최대 수치 / 해당 수치의 현재 수치 3가지의 인수를 입력하여 바의 채움을 결정
    protected void BarValueController(Slider bar, float maxValue, float curValue)
    {
        float per;

        per = curValue/ maxValue * 100;

        bar.value = per;
    }


    void Init()
    {
        

    }


}
