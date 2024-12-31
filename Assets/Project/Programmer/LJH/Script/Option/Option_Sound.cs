using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Option_Sound : Main_Option
{
    SettingManager setManager;

    Slider totalSoundBar;
    Slider bgmSoundBar;
    Slider effectSoundBar;

    struct ButtonStruct
    {
        public GameObject Button;
        public Slider Bar;
        public TMP_Text Text;
    }
    List<ButtonStruct> buttonStructs = new List<ButtonStruct>();
    GameObject[,] buttons;

    int sound_Ho = 0;
    int sound_Ver = 1;

    float preTotal;
    float newTotal;
    float defaultTotal;

    float preBgm;
    float newBgm;
    float defaultBgm;

    float preEffect;
    float newEffect;
    float defaultEffect;

    int _curIndex;
    void Start()
    {
        Init();
    }

    void Update()
    {
        //AudioVolumeCotroller();

        if (soundOnOff.activeSelf)
        {
            if (menuCo == null)
            {
                menuCo = StartCoroutine(Sound_Select());
            }
        }
    }

    private IEnumerator Sound_Select()
    {
        
        float x = Input.GetAxisRaw(InputKey.Horizontal);
        float y = Input.GetAxisRaw(InputKey.Vertical);

        ButtonStruct curButton = buttonStructs[_curIndex];
        for(int i = 0;  i < buttonStructs.Count; i++)
        {
            if (buttonStructs[i].Button == curButton.Button)
            {
                buttonStructs[i].Text.color = new Color(1, 0.5f, 0);
            }
            else
            {
                buttonStructs[i].Text.color = Color.white;
            }
        }
        if (buttonStructs[_curIndex].Bar != null)
        {
            // 아래 버튼 눌렀을 때
            if (y < 0)
            {
                _curIndex++;
                if (_curIndex >= buttonStructs.Count)
                {
                    _curIndex = buttonStructs.Count - 1;
                }
            }
            else if (y > 0)
            {
                _curIndex--;
                if (_curIndex < 0)
                {
                    _curIndex = 0;
                }
            }
            // 오른쪽 키 눌렀을 때
            if (x > 0)
            {
                buttonStructs[_curIndex].Bar.value += x * 0.05f;
            }
            else if (x < 0)
            {
                buttonStructs[_curIndex].Bar.value -= -x * 0.05f;
            }
        }
        // Bar 없을때
        else
        {
            if( x > 0)
            {
                _curIndex++;
                if (_curIndex >= buttonStructs.Count)
                {
                    _curIndex = buttonStructs.Count - 1;
                }
            }
            else if(x < 0)
            {
                _curIndex--;
                if (_curIndex < 3)
                {
                    _curIndex = 3;
                }
            }   
            // 위 누르면 2번 인덱스(슬라이드 버튼) 으로
            if( y > 0)
            {
                _curIndex = 2;
            }
        }
        //Todo : 수정 필요
        if (Input.GetButtonDown("Interaction"))
        {
            Debug.Log("E키 눌림");
            if (buttonStructs[_curIndex].Button == GetUI("CancelButton_sound"))
            {
                CancelButton();
            }
            //sound_Ver = 0;
            //sound_Ho = 1;
        }


        // 입력없으면 프레임마다
        if (x == 0 && y == 0)
        {
            yield return null;
        }
        else
        {
            yield return inputDelay.GetRealTimeDelay();
        }
        menuCo = null;
    }

    //void AudioVolumeCotroller()
    //{
    //    VolumeArrayCotroller(setManager.totalSoundSources, totalSoundBar);
    //    VolumeArrayCotroller(setManager.effectSources, effectSoundBar);
    //    setManager.bgmSource.volume = bgmSoundBar.value;

    //}

    //void VolumeArrayCotroller(AudioSource[] audioArray, Slider slider)
    //{
    //    for (int i = 0; i < audioArray.Length; i++)
    //    {
    //        audioArray[i].volume = slider.value;
    //    }
    //}
    public void AcceptButton()
    {
        VolumeCheck();
        totalSoundBar.value = newTotal;
        bgmSoundBar.value = newBgm;
        effectSoundBar.value = newEffect;

        preTotal = totalSoundBar.value;
        preBgm = bgmSoundBar.value;
        preEffect = effectSoundBar.value;


        //Todo : depth1으로 복귀
        soundOnOff.SetActive(false);
        UnActiveAllText();
    }

    void VolumeCheck()
    {
        newTotal = totalSoundBar.value;
        newBgm = bgmSoundBar.value;
        newEffect = effectSoundBar.value;
    }

    public void CancelButton()
    {
        totalSoundBar.value = preTotal;
        bgmSoundBar.value = preBgm;
        effectSoundBar.value = preEffect;

        //Todo : depth1으로 복귀
        soundOnOff.SetActive(false);
        UnActiveAllText();
    }

    public void DefaultButton()
    {
        // defaultPopUp.SetActive(true);
        // Todo: 팝업 과정 거쳐야함

        totalSoundBar.value = defaultTotal;
        bgmSoundBar.value = defaultBgm;
        effectSoundBar.value = defaultEffect;

        //Todo : depth1으로 복귀
        soundOnOff.SetActive(false);
        UnActiveAllText();
    }

    private void UnActiveAllText()
    {
        foreach(ButtonStruct buttonStruct in buttonStructs)
        {
            buttonStruct.Text.color = Color.white;
        }
    }

    private void Init()
    {
        buttons = new GameObject[4, 4];

        buttons[1, 0] = GetUI("Total Volume");
        buttons[2, 0] = GetUI("BGM Volume");
        buttons[3, 0] = GetUI("Effect Volume");
        buttons[0, 1] = GetUI("AcceptButton_sound");
        buttons[0, 2] = GetUI("CancelButton_sound");
        buttons[0, 3] = GetUI("DefaultButton_sound");

        buttonStructs.Add(GetButtonStruct(GetUI("Total Volume"), GetUI<Slider>("TotalVolumeBar"), GetUI<TMP_Text>("Total Volume")));
        buttonStructs.Add(GetButtonStruct(GetUI("BGM Volume"), GetUI<Slider>("BGMVolumeBar"), GetUI<TMP_Text>("BGM Volume")));
        buttonStructs.Add(GetButtonStruct(GetUI("Effect Volume"), GetUI<Slider>("EffectVolumeBar"), GetUI<TMP_Text>("Effect Volume")));
        buttonStructs.Add (GetButtonStruct(GetUI("AcceptButton_sound"), null,GetUI<TMP_Text>("AcceptButton_sound")));
        buttonStructs.Add(GetButtonStruct(GetUI("CancelButton_sound"), null, GetUI<TMP_Text>("CancelButton_sound")));
        buttonStructs.Add(GetButtonStruct(GetUI("DefaultButton_sound"), null, GetUI<TMP_Text>("DefaultButton_sound")));




        totalSoundBar = GetUI("TotalVolumeBar").GetComponent<Slider>();
        bgmSoundBar = GetUI("BGMVolumeBar").GetComponent<Slider>();
        effectSoundBar = GetUI("EffectVolumeBar").GetComponent<Slider>();


        totalSoundBar.value = 1;
        bgmSoundBar.value = 1;
        effectSoundBar.value = 1;

        soundOnOff = GetUI("SoundOnOff");

        defaultTotal = 100;
        defaultBgm = 100;
        defaultEffect = 100;

    }

    private ButtonStruct GetButtonStruct(GameObject button, Slider bar,TMP_Text text)
    {
        ButtonStruct newButtonStruct = new ButtonStruct();
        newButtonStruct.Button= button;
        newButtonStruct.Bar= bar;
        newButtonStruct.Text= text;
        return newButtonStruct;
    }
}
