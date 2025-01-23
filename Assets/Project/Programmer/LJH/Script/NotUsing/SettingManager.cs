using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [Header("�̴ϸ� ����")]
    [SerializeField] bool miniMapActivated;
    [SerializeField] bool miniMapFixed;

    //Language ���� �ʿ�

    //
    [Header("���� ����")]
    [SerializeField] public AudioSource[] totalSoundSources;

    [SerializeField] public AudioSource[] bgmSources;

    [SerializeField] public AudioSource[] effectSources;

    Slider total;
    Slider bgm;
    Slider effect;


    void Start()
    {
        DontDestroyOnLoad(this);

        if(this != null)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bgmSources[0].volume = bgm.value * total.value;

        for (int i = 0; i < effectSources.Length -1; i++)
        {
            effectSources[i].volume = effect.value * total.value;
        }
    }

    private void Init()
    {

    }
}
