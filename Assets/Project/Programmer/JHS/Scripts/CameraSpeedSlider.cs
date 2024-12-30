using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSpeedSlider : MonoBehaviour
{
    [SerializeField] private Slider cameraSpeedSlider;
    [SerializeField] private CameraSettingsSO cameraSettings;

    private void Start()
    {
        // 슬라이더 초기값 설정
        cameraSpeedSlider.value = cameraSettings.GetCameraSpeed();
        cameraSpeedSlider.onValueChanged.AddListener(OnCameraSpeedChanged);
    }

    private void OnCameraSpeedChanged(float value)
    {
        // ScriptableObject에 값 저장
        cameraSettings.SetCameraSpeed(value);
        Debug.Log($"Camera Rotate Speed updated: {value}");
    }
}
