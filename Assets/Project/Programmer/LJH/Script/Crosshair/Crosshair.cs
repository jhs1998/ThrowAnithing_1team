using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : BaseUI
{
    //  UI 리스트
    List<GameObject> uiList = new List<GameObject>();
    
    // 크로스헤어
    GameObject crossHair;

    private void Awake()
    {
        Bind();
        Init();
    }
    private void Update()
    {
        if(Time.timeScale == 0)
            crossHair.SetActive(false);
        else
            crossHair.SetActive(true);

        // 안써도 될거 같아서 보류
        //crossHair.SetActive(CrosshairOnOff());
    }

    // Comment : 안써도 될거 같아서 보류
    bool CrosshairOnOff()
    {
        for (int i = 0; i < uiList.Count; i++)
        {
            if (uiList[i].activeSelf)
            {
                return false;
            }

        }
        return true;
    }

    void Init()
    {
        crossHair = GetUI("Crosshair");

    }

}
