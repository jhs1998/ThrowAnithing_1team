using Zenject;
using UnityEngine;
using System.Collections;

public class SettingInstaller : MonoInstaller
{
    [SerializeField] public SettingManager setManager;
    public override void InstallBindings()
    {
        Container
            .Bind<SettingManager>()
            .FromInstance(setManager);
    }
}

public class Greeter
{
    public GameObject obj;
    public Greeter(GameObject _obj)
    {
        obj = _obj;
    }
}