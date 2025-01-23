using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Status : MonoBehaviour
{
    [SerializeField] Slider mpBar;
    [SerializeField] Slider chargingBar;
    //Comment : ü��,����,���׹̳� �� 1�� / �ش� ��ġ�� �ִ� ��ġ / �ش� ��ġ�� ���� ��ġ 3������ �μ��� �Է��Ͽ� ���� ä���� ����


    //Comment : �պ�����
    protected void BarValueController(Slider bar, float maxValue, float curValue)
    {
        if (bar == chargingBar)
        {
            if (bar.value < mpBar.value)
            {

                float per;

                per = curValue / maxValue * 100;

                bar.value = per;
            }

        }
        else
        {
            float per;

            per = curValue / maxValue * 100;

            bar.value = per;
        }
    }


    void Init()
    {


    }


}
