using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    private PlayerPresenter _playerPresenter;

    [Inject]
    public void Construct(PlayerPresenter playerPresenter)
    {
        _playerPresenter = playerPresenter;
    }

    private void FixedUpdate()
    {
        _playerPresenter.HandleMovement();
        _playerPresenter.HandleJump();
    }
}
