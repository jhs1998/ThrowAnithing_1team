using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Status : BaseStatus
{
    Image hpBar;
    Image mpBar;
    Image staminaBar;

    float maxHp;
    float curHp;

    float maxMp;
    float curMp;

    float maxStamina;
    float curStamina;

    private void Awake()
    {
        Bind();
    }
    private void Start()
    {
        Init();
    }

    //Comment : 체력,마나,스테미나 중 1개 / 해당 수치의 최대 수치 / 해당 수치의 현재 수치 3가지의 인수를 입력하여 바의 채움을 결정
    protected void BarValueController(Image bar, float maxValue, float curValue)
    {
        float per;

        per = curValue/ maxValue;

        bar.fillAmount = per;
    }


    void Init()
    {
        hpBar = GetImage("HpBar");
        mpBar = GetImage("MpBar");
        staminaBar = GetImage("StaminaBar");

    }


}
