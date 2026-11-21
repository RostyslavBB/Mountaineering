public class JumpingState : PlayerState
{
    private bool _hasLeftGround;

    public JumpingState(
        PlayerView playerView,
        PlayerModel playerModel,
        PlayerStateMachine playerStateMachine,
        PlayerAnimatorController playerAnimatorController)
        : base(playerView, playerModel, playerStateMachine, playerAnimatorController)
    {
    }

    public override void Enter()
    {
        _hasLeftGround = false;
        View.Jump(Model.JumpForce);

        AnimatorController.SetJumping(true);
    }

    public override void Exit()
    {
        AnimatorController.SetJumping(false);
    }

    public override void Update()
    {
        if (TryClimb()) return;

        if (!View.IsGrounded) _hasLeftGround = true;

        if (_hasLeftGround && View.IsGrounded)
        {
            if (TryWalk()) return;

            if (TryRunning()) return;

            if (TryIdle()) return;
        }
    }

    public override void FixedUpdate()
    {
        float speed = View.IsRunning
            ? Model.SprintSpeed
            : Model.MoveSpeed;

        View.Move(speed);

        AnimatorController.SetDirection(View.HorizontalInput);
    }
}
