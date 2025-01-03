using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ButtonActionInstaller : MonoBehaviour
{
    // 우선 버튼을 가져와서 그 버튼에 명령어를 넣어주는데
    // 현재 씬에서 프로젝트 컨텍스트의 OptionSetting의  OptionSave() OptionReset()를 넣어줘야한다

    // OptionSave()
    [SerializeField] public Button saveButton;
    // OptionLode()
    [SerializeField] public Button cancelButton;
    // OptionReset()
    [SerializeField] public Button resetButton;

    [Inject]
    public OptionSetting setting;

    private void Start()
    {
        saveButton.onClick.AddListener(setting.OptionSave);
        cancelButton.onClick.AddListener(setting.OptionLode);
        resetButton.onClick.AddListener(setting.OptionReset);
    }
}
