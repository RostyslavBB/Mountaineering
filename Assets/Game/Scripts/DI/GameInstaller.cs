using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerAnimator _playerAnimator;

    public override void InstallBindings()
    {
        Container.Bind<PlayerModel>().AsSingle();
        Container.Bind<PlayerStateMachine>().AsSingle();

        Container.Bind<PlayerPresenter>().AsSingle();
        Container.Bind<PlayerControls>().AsSingle();

        Container.Bind<PlayerView>()
            .FromInstance(_playerView)
            .AsSingle();

        Container.Bind<PlayerAnimator>()
            .FromInstance(_playerAnimator)
            .AsSingle();
    }
}
