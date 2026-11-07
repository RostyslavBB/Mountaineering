using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;

    private PlayerPresenter _playerPresenter;

    private void Awake()
    {
        PlayerModel playerModel = new();

        _playerPresenter = new PlayerPresenter(playerModel, _playerView);
    }

    private void FixedUpdate()
    {
        _playerPresenter.HandleMovement();
        _playerPresenter.HandleJump();
    }
}
