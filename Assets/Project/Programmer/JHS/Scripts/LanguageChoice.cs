using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LanguageChoce : MonoBehaviour
{
    [SerializeField] public TMP_Dropdown langaugeDropdown;

    [Inject]
    public OptionSetting setting;

    private void Awake()
    {
        // langaugeDropdown ���� ����� �� �̺�Ʈ ����
        CancellLanguage();
        langaugeDropdown.onValueChanged.AddListener(ChangeLanguage);
    }

    private void ChangeLanguage(int value)
    {
        // langaugeDropdown.value ���� setting.language�� ����
        setting.language = value;
    }

    public void ReturnLanguage()
    {
        langaugeDropdown.value = 0;
    }

    public void CancellLanguage()
    {
        langaugeDropdown.value = setting.language;
    }

    private void OnDestroy()
    {
        // �̺�Ʈ ���� ����
        langaugeDropdown.onValueChanged.RemoveListener(ChangeLanguage);
    }
}
