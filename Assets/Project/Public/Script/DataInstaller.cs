using UnityEngine;
using Zenject;

public class DataInstaller : MonoInstaller
{
    [SerializeField] private GlobalPlayerData globalData;
    [SerializeField] private PlayerData playerData;

    public override void InstallBindings()
    {
        Container.Bind<GlobalPlayerData>()
            .FromInstance(globalData);

        Container.Bind<PlayerData>()
            .FromInstance(playerData);
    }
}