using System.Collections;
using System.Collections.Generic;
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


    void Start()
    {
        Init();
    }

    void Update()
    {
        AudioVolumeCotroller();
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
        totalSoundBar = GetUI("TotalVolumeBar").GetComponent<Slider>();
        bgmSoundBar = GetUI("BGMVolumeBar").GetComponent<Slider>();
        effectSoundBar = GetUI("EffectVolumeBar").GetComponent<Slider>();


        totalSoundBar.value = 1;
        bgmSoundBar.value = 1;
        effectSoundBar.value = 1;

    }
}
