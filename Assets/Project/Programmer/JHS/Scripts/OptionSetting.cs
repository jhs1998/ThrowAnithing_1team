using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class OptionSetting
{
    // 설정값 저장
    // 효과음 배경음
    [Range(0, 100)]
    public float effectSound;
    [Range(0, 100)]
    public float backgroundSound;

    // 화면 속도 감도 1~5
    [Range(0.1f, 5)]
    public float cameraSpeed;

    // 미니맵 온오프 기능 저장
    public bool minimapOn;
}
