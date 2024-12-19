using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    private string savePath; // 필드 초기화 시 Application.persistentDataPath 호출 금지

    private void Awake()
    {
        // Unity API는 Awake 또는 Start에서 호출
        savePath = Application.persistentDataPath + "/save";
    }

    public override void InstallBindings()
    {
        // UserDataManager와 savePath를 주입
        Container.Bind<string>().FromInstance(savePath).AsSingle(); // 경로 주입
        Container.Bind<UserDataManager>().FromComponentInNewPrefabResource("UserDataManagerPrefab").AsSingle(); // UserDataManager 인스턴스 주입
        Container.Bind<SlotManager>().FromComponentInHierarchy().AsSingle(); // SlotManager 주입
    }
}
