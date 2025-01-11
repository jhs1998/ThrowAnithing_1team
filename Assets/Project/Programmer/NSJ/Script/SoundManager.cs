using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : BaseBinder
{
    public static SoundManager Instance;

    public static AudioMixer Mixer { get { return Instance._audioMixer; } }
    public static AudioSource Master { get { return Instance._master; } }
    public static AudioSource BGM { get { return Instance._bgm; } }
    public static AudioSource SFX { get { return Instance._sfx; } }
    public static AudioSource LoopSFX { get { return Instance._loopSfx; } }

    [SerializeField] private AudioMixer _audioMixer;
    private AudioSource _master;
    private AudioSource _bgm;
    private AudioSource _sfx;
    private AudioSource _loopSfx;

    [SerializeField]private float volumeValue;
    [SerializeField]private float masterVolumeValue;
    private static float VolumeValue { get { return Instance.volumeValue; } }
    private static float MasterVolumeValue { get { return Instance.masterVolumeValue; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Bind();
        _master = GetObject<AudioSource>("Master");
        _bgm = GetObject<AudioSource>("BGM");
        _sfx = GetObject<AudioSource>("SFX");
        _loopSfx = GetObject<AudioSource>("LoopSFX");
    }


    public static void SetVolumeMaster(float volume)
    {
        Mixer.SetFloat("Master",  (-MasterVolumeValue + volume * MasterVolumeValue) +20);
    }
    public static void SetVolumeBGM(float volume)
    {
        Mixer.SetFloat("BGM", (-VolumeValue + volume * VolumeValue));
    }
    public static void SetVolumeSFX(float volume)
    {
        Mixer.SetFloat("SFX", (-VolumeValue + volume * VolumeValue));
    }
    public static void SetVolumeLoopSFX(float volume)
    {
        Mixer.SetFloat("LoopSFX", (-VolumeValue + volume * VolumeValue));
    }
    public static float GetVolumeMaster()
    {
        Mixer.GetFloat("Master", out float volume);
        return (volume+ MasterVolumeValue - 20)/ MasterVolumeValue;
    }
    public static float GetVolumeBGM()
    {
        Mixer.GetFloat("BGM", out float volume);
        return (volume + VolumeValue) / VolumeValue;
    }
    public static float GetVolumeSFX()
    {
        Mixer.GetFloat("SFX", out float volume);
        return (volume + VolumeValue) / VolumeValue;
    }
    public static float GetVolumeLoopSFX()
    {
        Mixer.GetFloat("LoopSFX", out float volume);
        return (volume + VolumeValue) / VolumeValue;
    }

    public static void PlayBGM(AudioClip clip)
    {
        if (clip == null)
            return;

        BGM.clip = clip;
        BGM.Play();
    }
    public static void PauseBGM()
    {
        BGM.Pause();
    }
    public static void StopBGM()
    {
        BGM.Pause();
    }

    public static void PlaySFX(AudioClip clip)
    {
        if (clip == null)
            return;

        SFX.PlayOneShot(clip);
    }

    public static void PlayLoopSFX(AudioClip clip)
    {
        if(clip == null)
            return;

        LoopSFX.PlayOneShot(clip);
    }
}
