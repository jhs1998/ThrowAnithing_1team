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

    // OptionSave()
    [SerializeField] public Button saveSoundButton;
    // OptionLode()
    [SerializeField] public Button cancelSoundButton;
    // OptionReset()
    [SerializeField] public Button resetSoundButton;

    // 미니맵 클릭이벤트 추가
    [SerializeField] public Button MinimapOn;
    [SerializeField] public Button MinimapOff;
    [SerializeField] public Button MiniMapFixOn;
    [SerializeField] public Button MiniMapFixOff;
    [Inject]
    public OptionSetting setting;

    private void Start()
    {
        saveButton.onClick.AddListener(setting.OptionSave);
        cancelButton.onClick.AddListener(setting.OptionLode);
        resetButton.onClick.AddListener(() =>
        {
            setting.OptionReset(); // OptionReset 실행
            FindObjectOfType<CameraSpeedSlider>()?.ResetCameraSpeed(); // 슬라이더 강제 갱신
        });
        saveSoundButton.onClick.AddListener(setting.OptionSave);
        cancelSoundButton.onClick.AddListener(setting.OptionLode);
        resetSoundButton.onClick.AddListener(setting.OptionReset);
        MinimapOn.onClick.AddListener(setting.MinimapOn);
        MinimapOff.onClick.AddListener(setting.MinimapOff);
        MiniMapFixOn.onClick.AddListener(setting.MiniMapFixOn);
        MiniMapFixOff.onClick.AddListener(setting.MiniMapFixOff);
    }
}
