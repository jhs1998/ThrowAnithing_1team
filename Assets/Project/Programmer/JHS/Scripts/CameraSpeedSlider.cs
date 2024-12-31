using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CameraSpeedSlider : MonoBehaviour
{
    [Range(0.1f, 5f)]
    [SerializeField] private Slider cameraSpeedSlider;

    [Inject]
    private OptionSetting setting;

    private void Start()
    {
        cameraSpeedSlider = GetComponent<Slider>();

        // json으로 당겨온 속도 값으로 변경
        cameraSpeedSlider.value = setting.cameraSpeed;

        // 슬라이더 값 변경 시 SettingCameraSpeed 메서드 호출
        cameraSpeedSlider.onValueChanged.AddListener(SettingCameraSpeed);
    }
    public void SettingCameraSpeed(float value)
    {
        setting.cameraSpeed = value;
    }

    private void OnDestroy()
    {
        // 이벤트 리스너 해제
        cameraSpeedSlider.onValueChanged.RemoveListener(SettingCameraSpeed);
    }
}
