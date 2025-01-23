using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TotalVolumeBar : MonoBehaviour
{
    [SerializeField]
    private Slider totalVolumeSlider;

    [Inject]
    public OptionSetting setting;

    private void Awake()
    {
        if (totalVolumeSlider == null)
        {
            totalVolumeSlider = GetComponent<Slider>();
        }
    }
    private void Start()
    {
        totalVolumeSlider.minValue = 0f;
        totalVolumeSlider.maxValue = 1f;
        totalVolumeSlider.value = Mathf.Clamp(setting.wholesound, totalVolumeSlider.minValue, totalVolumeSlider.maxValue);

        
        totalVolumeSlider.onValueChanged.AddListener(SettingTotalVolume);
    }
    public void SettingTotalVolume(float value)
    {
        setting.wholesound = value;
        SoundManager.SetVolumeMaster(value * 100);
    }

    private void OnDestroy()
    {
        // �̺�Ʈ ������ ����
        if (totalVolumeSlider != null)
        {
            totalVolumeSlider.onValueChanged.RemoveListener(SettingTotalVolume);
        }
    }

    // ���� ��ư ������ ui ����
    public void ResetTotalVolume()
    {
        totalVolumeSlider.value = Mathf.Clamp(setting.wholesound, totalVolumeSlider.minValue, totalVolumeSlider.maxValue);
        totalVolumeSlider.SetValueWithoutNotify(totalVolumeSlider.value);
    }
}
