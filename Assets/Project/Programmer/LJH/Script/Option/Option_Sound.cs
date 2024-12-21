using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Option_Sound : Main_Option
{
    [Inject]
    SettingManager setManager;

    Slider totalSoundBar;
    Slider bgmSoundBar;
    Slider effectSoundBar;

    GameObject[,] buttons;

    int sound_Ho = 0;
    int sound_Ver = 1;



    void Start()
    {
        Init();
    }

    void Update()
    {
        AudioVolumeCotroller();

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
        float x = Input.GetAxisRaw("Horizontal");
        float y = -Input.GetAxisRaw("Vertical");

        if (sound_Ho == 0)
        {

            switch (sound_Ver)
            {
                case 1:
                    totalSoundBar.value += x * 0.05f;
                    break;

                case 2:
                    bgmSoundBar.value += x * 0.05f;
                    break;

                case 3:
                    effectSoundBar.value += x * 0.05f;
                    break;
            }
        }

        sound_Ho += (int)x;
        sound_Ver += (int)y;

        Debug.Log($"{sound_Ho} {sound_Ver}");

        if (sound_Ver != 0)
        {
            if (x != 0)
            {
                sound_Ho = 0;
            }
        }

        if (sound_Ho != 0)
        {
            if (y != 0)
            {
                sound_Ho = 0;
                sound_Ver = 1;
            }
        }

        if (sound_Ver <= 0)
        {
            if (sound_Ho == 4)
                sound_Ho = 1;

            if (sound_Ho <= 0)
                sound_Ho = 3;

        }

        if (sound_Ho == 0)
        {
            if (sound_Ver == 4)
                sound_Ver = 1;

            if (sound_Ver <= 0)
                sound_Ver = 3;

        }

        //Todo : 수정 필요
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E키 눌림");
            sound_Ver = 0;
            sound_Ho = 1;
        }


        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (buttons[i, j] == null)
                    continue;

                buttons[i, j].GetComponent<TMP_Text>().color = Color.white;
            }
        }

        buttons[sound_Ver, sound_Ho].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
        yield return inputDelay.GetDelay();
        menuCo = null;


    }

    void AudioVolumeCotroller()
    {
        VolumeArrayCotroller(setManager.totalSoundSources, totalSoundBar);
        VolumeArrayCotroller(setManager.effectSources, effectSoundBar);
        setManager.bgmSource.volume = bgmSoundBar.value;

    }

    void VolumeArrayCotroller(AudioSource[] audioArray, Slider slider)
    {
        for (int i = 0; i < audioArray.Length; i++)
        {
            audioArray[i].volume = slider.value;
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



        totalSoundBar = GetUI("TotalVolumeBar").GetComponent<Slider>();
        bgmSoundBar = GetUI("BGMVolumeBar").GetComponent<Slider>();
        effectSoundBar = GetUI("EffectVolumeBar").GetComponent<Slider>();


        totalSoundBar.value = 1;
        bgmSoundBar.value = 1;
        effectSoundBar.value = 1;

        soundOnOff = GetUI("SoundOnOff");

    }
}
