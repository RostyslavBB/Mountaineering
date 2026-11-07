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
        float speed = _view.IsSprinting
            ? _model.SprintSpeed
            : _model.MoveSpeed;

        _view.Move(speed);
    }

    public void HandleJump()
    {
        if (!_view.ConsumeJumpRequst())
            return;

        if (!_view.IsGrounded)
            return;

        _view.Jump(_model.JumpForce);
    }
}
