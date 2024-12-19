using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    bool miniMapActed;
    bool miniMapFixed;

    //Language 변수 필요

    //
    [Header("사운드 관련")]
    [SerializeField] public AudioSource[] totalSoundSources;

    [SerializeField] public AudioSource bgmSource;
    [SerializeField] public AudioClip bgmClip;

    [SerializeField] public AudioSource[] effectSources;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Init()
    {
        bgmSource.clip = bgmClip;

        totalSoundSources[0].volume = 1;

    }
}
