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

    [Header("사운드 관련")]
    [SerializeField] AudioSource[] totalSoundSources;

    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioClip bgmClip;

    [SerializeField] AudioSource[] effectSources;

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
        VolumeArrayCotroller(totalSoundSources, totalSoundBar);
        VolumeArrayCotroller(effectSources, effectSoundBar);
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

        bgmSource.clip = bgmClip;

        totalSoundBar.value = 1;
        bgmSoundBar.value = 1;
        effectSoundBar.value = 1;

    }
}
