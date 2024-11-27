using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindManagers();
    }

    private void BindManagers()
    {
        Container.Bind<UIManager>()
            .FromNewComponentOnNewGameObject()
            .WithGameObjectName("UIManager")
            .AsSingle()
            .NonLazy();
    }
}
