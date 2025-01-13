using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Sound/Data")]
public partial class SoundData : ScriptableObject
{
    [System.Serializable]
    public struct BGMStruct
    {
        public AudioClip Main;
        public AudioClip Lobby;
    }
    [SerializeField] private BGMStruct _bgm;
    public BGMStruct BGM { get { return _bgm; } }

    [System.Serializable]
    public struct UISFXStruct
    {
        public AudioClip ButtonClick;
        public AudioClip Cancel;
    }
    [SerializeField] private UISFXStruct _ui;
    public UISFXStruct UI { get { return _ui; } }
}
