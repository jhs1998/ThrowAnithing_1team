using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CameraSpeedSlider : MonoBehaviour
{
    [SerializeField]
    private Slider cameraSpeedSlider;

    [Inject]
    public OptionSetting setting;

    private void Awake()
    {
        if (cameraSpeedSlider == null)
        {
            cameraSpeedSlider = GetComponent<Slider>();
        }
    }
    private void Start()
    {
        cameraSpeedSlider.minValue = 0.01f;
        cameraSpeedSlider.maxValue = 3f;
        cameraSpeedSlider.value = Mathf.Clamp(setting.cameraSpeed, cameraSpeedSlider.minValue, cameraSpeedSlider.maxValue);

        // �����̴� �� ���� �� SettingCameraSpeed �޼��� ȣ��
        cameraSpeedSlider.onValueChanged.AddListener(SettingCameraSpeed);
    }
    public void SettingCameraSpeed(float value)
    {
        setting.cameraSpeed = value;
    }

    // ��Ȱ��ȭ �� �̺�Ʈ ����
    private void OnDestroy()
    {
        // �̺�Ʈ ������ ����
        if (cameraSpeedSlider != null)
        {
            cameraSpeedSlider.onValueChanged.RemoveListener(SettingCameraSpeed);
        }
    }

    // ����, ��� ��ư ������ ui ����
    public void ResetCameraSpeed()
    {
        cameraSpeedSlider.value = Mathf.Clamp(setting.cameraSpeed, cameraSpeedSlider.minValue, cameraSpeedSlider.maxValue);
        cameraSpeedSlider.SetValueWithoutNotify(cameraSpeedSlider.value);
    }
}
