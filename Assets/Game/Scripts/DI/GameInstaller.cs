using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private PlayerView _playerView;

    public override void InstallBindings()
    {
        Container.Bind<PlayerModel>().AsSingle();
        Container.Bind<PlayerPresenter>().AsSingle();
        Container.Bind<PlayerControls>().AsSingle();
        Container.Bind<PlayerView>().FromInstance(_playerView).AsSingle();
    }
}
