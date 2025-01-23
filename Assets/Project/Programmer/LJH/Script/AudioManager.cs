/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Option_Sound option_Sound;

    [Header("�ν����Ϳ� �ҽ��� Ŭ���� ����־ ���")]
    [Header("�������")]
    [SerializeField] public List<AudioSource> bgmList;
    [SerializeField] public List<AudioClip> bgmClips;

    [Header("ȿ����")]
    [SerializeField] public List<AudioSource> effectList;
    [SerializeField] public List<AudioClip> effectClips;

    public static AudioManager instance = null;

    void Awake()
    {
        Singleton();
        Init();
    }

    private void Start()
    {
        option_Sound.AudioVolumeCotroller();
        Play(bgmList[0]);
    }

    // bgmList or effectList �ε����� �ҷ��ͼ� ������ ��
    public void Play(AudioSource sound)
    {
        sound.Play();
    }


    void Init()
    {
        for (int i = 0; i < bgmList.Count; i++)
        {
            bgmList[i].clip = bgmClips[i];
        }

        for (int i = 0; i < effectList.Count; ++i) {
            effectList[i].clip = effectClips[i];
        }
    }

    void Singleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
*/