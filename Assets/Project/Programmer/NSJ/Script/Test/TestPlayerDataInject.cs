using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class TestPlayerDataInject : MonoBehaviour
{
    [Inject]
    PlayerData _playerData;
    [Inject]
    GlobalPlayerStateData _globalStateData;
    [Inject]
    GlobalGameData _gameData;

    [SerializeField] SceneField scene;
    private void Awake()
    {
        _globalStateData.NewPlayerSetting();
        _playerData.CopyGlobalPlayerData(_globalStateData, _gameData);
        if (scene != null)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
