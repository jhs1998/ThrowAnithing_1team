using Assets.Project.Programmer.NSJ.RND.Script.ZenjectTest;
using UnityEngine;
using Zenject;

namespace Assets.Project.Programmer.NSJ.RND.Script.Test.ZenjectTest
{
    public class UntitledInstaller : MonoInstaller
    {
        [SerializeField] JenjectTester tester;

        Item item;
        public override void InstallBindings()
        {
            Container
                .Bind<JenjectTester>()
                .FromInstance(tester);

            Container.Bind<IDamagable>()
                .To<Item>()
                .FromInstance(item);
        }
    }

    public class Item : IDamagable
    {
        public void TakeDamage()
        {
            Debug.Log("¶§¸²");
        }
    }
    public interface IDamagable
    {
        void TakeDamage();
    }
}