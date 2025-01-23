using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EffectVolume : MonoBehaviour
{
    [SerializeField]
    private Slider effectVolume;

    [Inject]
    public OptionSetting setting;

    private void Awake()
    {
        if (effectVolume == null)
        {
            effectVolume = GetComponent<Slider>();
        }
    }
    private void Start()
    {
        effectVolume.minValue = 0f;
        effectVolume.maxValue = 1f;
        effectVolume.value = Mathf.Clamp(setting.effectSound, effectVolume.minValue, effectVolume.maxValue);

        effectVolume.onValueChanged.AddListener(SettingEffectVolume);
    }
    public void SettingEffectVolume(float value)
    {
        setting.effectSound = value;
        SoundManager.SetVolumeSFX(value);
    }

    private void OnDestroy()
    {
        // �̺�Ʈ ������ ����
        if (effectVolume != null)
        {
            effectVolume.onValueChanged.RemoveListener(SettingEffectVolume);
        }
    }

    // ���� ��ư ������ ui ����
    public void ResetTotalVolume()
    {
        effectVolume.value = Mathf.Clamp(setting.effectSound, effectVolume.minValue, effectVolume.maxValue);
        effectVolume.SetValueWithoutNotify(effectVolume.value);
    }
}
