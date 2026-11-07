public class PlayerPresenter
{
    private readonly PlayerModel _model;
    private readonly PlayerView _view;

    public PlayerPresenter(PlayerModel model, PlayerView view)
    {
        _model = model;
        _view = view;
    }

    public void HandleMovement()
    {
        if (_model.State == PlayerState.Climbing)
        {
            _view.Climb(_view.ClimbSpeed);

            if (_view.HorizontalInput != 0)
                StopClimbing();

            return;
        }

        float speed = _view.IsSprinting
            ? _model.SprintSpeed
            : _model.MoveSpeed;

        _view.Move(speed);
    }

    public void HandleJump()
    {
        if (_model.State == PlayerState.Climbing)
        {
            _view.ConsumeJumpRequest();
            return;
        }

        if (!_view.ConsumeJumpRequest())
            return;

        if (!_view.IsGrounded)
            return;

        _view.Jump(_model.JumpForce);
    }

    public void HandleInteraction()
    {
        if (!_view.ConsumeInteractRequest())
        {
            return;
        }

        if (_model.State == PlayerState.Climbing)
        {
            StopClimbing();
            return;
        }

        if (!_view.CanClimb)
        {
            return;
        }

        StartClimbing();
    }

    private void StartClimbing()
    {
        _model.SetState(PlayerState.Climbing);
        _view.SetClimbing(true);
    }

    private void StopClimbing()
    {
        _model.SetState(PlayerState.Idle);
        _view.SetClimbing(false);
    }
}
