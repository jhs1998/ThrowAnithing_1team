using UnityEngine;
using Zenject;

public class DontDestroyInstaller : MonoInstaller
{
    public static DontDestroyInstaller Instance;
    [SerializeField] DataContainer container;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public override void InstallBindings()
    {
        Container.Bind<PlayerData>()
            .FromInstance(container.PlayerData)
            .AsSingle();
    }
}