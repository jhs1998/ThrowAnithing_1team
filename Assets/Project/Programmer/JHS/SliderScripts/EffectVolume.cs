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
        effectVolume.maxValue = 100f;
        effectVolume.value = Mathf.Clamp(setting.effectSound, effectVolume.minValue, effectVolume.maxValue);

        effectVolume.onValueChanged.AddListener(SettingTotalVolume);
    }
    public void SettingTotalVolume(float value)
    {
        setting.effectSound = value;
    }

    private void OnDestroy()
    {
        // 이벤트 리스너 해제
        if (effectVolume != null)
        {
            effectVolume.onValueChanged.RemoveListener(SettingTotalVolume);
        }
    }
}
