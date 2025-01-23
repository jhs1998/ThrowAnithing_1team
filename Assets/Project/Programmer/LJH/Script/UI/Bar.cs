using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : Status
{
    //Todo : ���� �� ���� ĳ���ͷ� �����ؾ���
    [SerializeField] LJH_inputManager player;

    float maxNum;
    float curNum;

    private void Update()
    {
        switch (this.gameObject.name)
        {
            case "HpBar":
                maxNum = player.maxHp;
                curNum = player.curHp;
                break;

            case "MpBar":
                maxNum = player.maxMp;
                curNum = player.curMp;
                break;

            case "ChargingMpBar" :
                maxNum = player.curMp;
                curNum = player.curCharging;
                break;

            case "StaminaBar":
                maxNum = player.maxSta;
                curNum = player.curSta;
                break;
        }

        BarValueController(this.GetComponent<Slider>(), maxNum, curNum);

    }
}
