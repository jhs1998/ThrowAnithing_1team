using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[System.Serializable]
public class OptionSetting
{
    // 설정값 저장
    // 효과음 배경음
    [Range(0, 100)]
    public float effectSound;
    [Range(0, 100)]
    public float backgroundSound;

    // 화면 속도 감도 1~5
    [Range(0.1f, 5)]
    public float cameraSpeed;

    // 미니맵 온오프 기능 저장 1 = on , 0 = off
    public int miniMapOn;


    // 옵션 세팅 수치 세이브
    public void OptionSave()
    {
        PlayerPrefs.SetFloat("EffectSound", effectSound);
        PlayerPrefs.SetFloat("BackgroundSound", backgroundSound);
        PlayerPrefs.SetFloat("CameraSpeed", cameraSpeed);
        PlayerPrefs.SetInt("MiniMapOn", miniMapOn);
    }

    // 옵션 세팅 수치 로드
    public void OptionLode()
    {
        if (!PlayerPrefs.HasKey("EffectSound") || !PlayerPrefs.HasKey("BackgroundSound") || !PlayerPrefs.HasKey("CameraSpeed") || !PlayerPrefs.HasKey("miniMapOn"))
        {
            effectSound = 100;
            backgroundSound = 100;
            cameraSpeed = 5;
            miniMapOn = 1;
            return;
        }
        effectSound = PlayerPrefs.GetFloat("EffectSound");
        backgroundSound = PlayerPrefs.GetFloat("BackgroundSound");
        cameraSpeed = PlayerPrefs.GetFloat("CameraSpeed");
        miniMapOn = PlayerPrefs.GetInt("MiniMapOn");
    }
}
