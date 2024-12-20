using UnityEngine;
using Zenject;

public class DataInstaller : MonoInstaller
{
    [SerializeField] private GlobalPlayerData globalData;
    [SerializeField] private UserDataManager userDataManager;
    [SerializeField] private PlayerData playerData;

    public override void InstallBindings()
    {
        Container.Bind<GlobalPlayerData>().
            FromInstance(globalData).
            AsSingle().
            NonLazy();

        Container.Bind<UserDataManager>().
            FromInstance(userDataManager).
            AsSingle().
            NonLazy();

        Container.Bind<PlayerData>()
            .FromInstance(playerData);
    }
}