using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    private PlayerModel _model;
    private PlayerView _view;

    private void Awake()
    {
        _model = new PlayerModel();
        _view = GetComponent<PlayerView>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float speed = _view.IsSprinting
            ? _model.SprintSpeed
            : _model.MoveSpeed;

        _view.Move(speed);
    }

    private void HandleJump()
    {
        if (!_view.ConsumeJumpRequst())
            return;

        if (!_view.IsGrounded)
            return;

        _view.Jump(_model.JumpForce);
    }
}
