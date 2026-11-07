using UnityEngine;
using Zenject;

public class PlayerSettingsInstaller : MonoInstaller
{
    [SerializeField] private PlayerSettings _settings;

    public override void InstallBindings()
    {
        Container.Bind<PlayerSettings>()
            .FromInstance(_settings)
            .AsSingle();
    }
}
