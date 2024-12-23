using UnityEngine;
using Zenject;

public class DataInstaller : MonoInstaller
{
    [SerializeField] private GlobalPlayerData globalData;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private UserDataManager userDataManager;
    public override void InstallBindings()
    {
        Container.Bind<GlobalPlayerData>()
            .FromInstance(globalData)
            .AsSingle();

        Container.Bind<PlayerData>()
            .FromInstance(playerData);

        Container.Bind<UserDataManager>()
            .FromComponentInNewPrefab(userDataManager)
            .AsSingle();
    }
}