using Assets.Project.Programmer.NSJ.RND.Script.ZenjectTest;
using UnityEngine;
using Zenject;

public class UntitledInstaller : MonoInstaller
{
    [SerializeField] JenjectTester tester;
    public override void InstallBindings()
    {
        Container
            .Bind<JenjectTester>()
            .FromInstance(tester);
    }
}