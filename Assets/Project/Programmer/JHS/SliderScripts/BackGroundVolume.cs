using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BackGroundVolume : MonoBehaviour
{
    [SerializeField]
    private Slider backgroundVolume;

    [Inject]
    public OptionSetting setting;

    private void Awake()
    {
        if (backgroundVolume == null)
        {
            backgroundVolume = GetComponent<Slider>();
        }
    }
    private void Start()
    {
        backgroundVolume.minValue = 0f;
        backgroundVolume.maxValue = 1f;
        backgroundVolume.value = Mathf.Clamp(setting.backgroundSound, backgroundVolume.minValue, backgroundVolume.maxValue);

        backgroundVolume.onValueChanged.AddListener(SettingBackGroundVolume);
    }
    public void SettingBackGroundVolume(float value)
    {
        setting.backgroundSound = value;
        SoundManager.SetVolumeBGM(value);
    }

    private void OnDestroy()
    {
        // �̺�Ʈ ������ ����
        if (backgroundVolume != null)
        {
            backgroundVolume.onValueChanged.RemoveListener(SettingBackGroundVolume);
        }
    }
    // ���� ��ư ������ ui ����
    public void ResetBackGroundVolume()
    {
        backgroundVolume.value = Mathf.Clamp(setting.backgroundSound, backgroundVolume.minValue, backgroundVolume.maxValue);
        backgroundVolume.SetValueWithoutNotify(backgroundVolume.value);
    }
}
