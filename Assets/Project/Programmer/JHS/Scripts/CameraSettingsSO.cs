using UnityEngine;

[CreateAssetMenu(fileName = "CameraSettings", menuName = "Settings/CameraSettings")]
public class CameraSettingsSO : ScriptableObject
{
    [Range(0.1f, 5f)] public float cameraRotateSpeed = 1.0f;

    // 감도 값을 저장
    public void SetCameraSpeed(float speed)
    {
        cameraRotateSpeed = Mathf.Clamp(speed, 0.1f, 5f);
    }

    // 감도 값 반환
    public float GetCameraSpeed()
    {
        return cameraRotateSpeed;
    }
}
