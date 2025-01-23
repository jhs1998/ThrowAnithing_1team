using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OptionSetting : MonoBehaviour
{
    // ������ ����
    // ȿ���� ����� ��ü����
    [Range(0, 1f)]
    public float effectSound;
    [Range(0, 1f)]
    public float backgroundSound;
    [Range(0, 1f)]
    public float wholesound;

    // ȭ�� �ӵ� ���� 1~5
    [Range(0.01f, 3f)]
    public float cameraSpeed = 3;

    // �̴ϸ� �¿��� ��� ���� 1 = on , 0 = off
    public bool miniMapOnBool;
    public int miniMapOn;
    // �̴ϸ� ���� ��� ���� 1 = on , 0 = off
    public bool miniMapFixBool;
    public int miniMapFix;

    // ���� ��� �߰� 0 = korea, 1 = english
    public int language;

    private const string EffectSoundKey = "Option_EffectSound";
    private const string BackgroundSoundKey = "Option_BackgroundSound";
    private const string WholeSoundKey = "Option_WholeSound";
    private const string CameraSpeedKey = "Option_CameraSpeed";
    private const string MiniMapOnKey = "Option_MiniMapOn";
    private const string MiniMapFixKey = "Option_MiniMapFix";
    private const string LanguageKey = "Option_LanguageKey";

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs �ʱ�ȭ �Ϸ�");
    }

    // �ɼ� ���� ��ġ ���̺�
    public void OptionSave()
    {
        if (miniMapOnBool == true)
            miniMapOn = 1;
        else miniMapOn = 0;
        if (miniMapFixBool == true)
            miniMapFix = 1;
        else miniMapFix = 0;
        PlayerPrefs.SetFloat(EffectSoundKey, effectSound);
        PlayerPrefs.SetFloat(BackgroundSoundKey, backgroundSound);
        PlayerPrefs.SetFloat(WholeSoundKey, wholesound);
        PlayerPrefs.SetFloat(CameraSpeedKey, cameraSpeed);
        PlayerPrefs.SetInt(MiniMapOnKey, miniMapOn);
        PlayerPrefs.SetInt(MiniMapFixKey, miniMapFix);
        PlayerPrefs.SetInt(LanguageKey, language);     
        PlayerPrefs.Save();
        Debug.Log("�ɼ� ���� ����");
    }

    // �ɼ� ���� ��ġ �ε�
    public void OptionLoad()
    {
        if (!PlayerPrefs.HasKey(EffectSoundKey) || !PlayerPrefs.HasKey(BackgroundSoundKey) || !PlayerPrefs.HasKey(WholeSoundKey) || !PlayerPrefs.HasKey(CameraSpeedKey) 
            || !PlayerPrefs.HasKey(MiniMapOnKey) || !PlayerPrefs.HasKey(MiniMapFixKey) || !PlayerPrefs.HasKey(LanguageKey))
        {
            Debug.Log("�⺻ ���� �Ϸ�");
            OptionResetAll();
            return;
        }
        Debug.Log("�ɼ� ���� �ҷ�����");       
        effectSound = PlayerPrefs.GetFloat(EffectSoundKey);
        backgroundSound = PlayerPrefs.GetFloat(BackgroundSoundKey);
        wholesound = PlayerPrefs.GetFloat(WholeSoundKey);
        cameraSpeed = PlayerPrefs.GetFloat(CameraSpeedKey);
        miniMapOn = PlayerPrefs.GetInt(MiniMapOnKey);
        miniMapFix = PlayerPrefs.GetInt(MiniMapFixKey);
        language = PlayerPrefs.GetInt(LanguageKey);
        if (miniMapOn == 1)
            miniMapOnBool = true;
        else miniMapOnBool = false;
        if (miniMapFix == 1)
            miniMapFixBool = true;
        else miniMapFixBool = false;        
    }

    public void OptionReset(int value)
    {
        if (value == 1)
        {
            cameraSpeed = 3;
            miniMapOnBool = true;
            miniMapFixBool = true;
            language = 0;
        }
        else if (value == 2)
        {
            effectSound = 1f;
            backgroundSound = 1f;
            wholesound = 1f;
        }         
        OptionSave();
    }
    public void OptionResetAll()
    {
        cameraSpeed = 3;
        miniMapOnBool = true;
        miniMapFixBool = true;
        language = 0;
        effectSound = 1f;
        backgroundSound = 1f;
        wholesound = 1f;
        OptionSave();
    }
    public void MinimapOn()
    {
        miniMapOnBool = true;
        Debug.Log("�̴ϸ� On");
    }

    public void MinimapOff()
    {
        miniMapOnBool = false;
        Debug.Log("�̴ϸ� Off");
    }

    public void MiniMapFixOn()
    {
        miniMapFixBool = true;
        Debug.Log("�̴ϸ��Ƚ� On");
    }

    public void MiniMapFixOff()
    {
        miniMapFixBool = false;
        Debug.Log("�̴ϸ��Ƚ� Off");
    }
}
