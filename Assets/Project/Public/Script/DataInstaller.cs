using System.Reflection;
using UnityEngine;
using Zenject;

public class DataInstaller : MonoInstaller
{
    [SerializeField] private GlobalGameData globalData;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private UserDataManager userDataManager;
    [SerializeField] private GlobalPlayerStateData globalPlayerStateData;
    [SerializeField] private LobbyUpGrade lobbyUpGrade;
        public override void InstallBindings()
    {
        Container.Bind<GlobalGameData>()
            .FromInstance(globalData)
            .AsSingle();

        Container.Bind<PlayerData>()
            .FromInstance(playerData);

        Container.Bind<UserDataManager>()
            .FromInstance(userDataManager)
            .AsSingle();

        Container.Bind<GlobalPlayerStateData>()
            .FromInstance(globalPlayerStateData)
            .AsSingle();

        Container.Bind<LobbyUpGrade>()
            .FromInstance(lobbyUpGrade)
            .AsSingle();
    }
}