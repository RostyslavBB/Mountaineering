public class WalkingState : PlayerState
{
    public WalkingState(
        PlayerView playerView,
        PlayerModel playerModel,
        PlayerStateMachine playerStateMachine,
        PlayerAnimatorController playerAnimatorController)
        : base(playerView, playerModel, playerStateMachine, playerAnimatorController)
    {
    }

    public override void Enter()
    {
        AnimatorController.SetMoveSpeed(1f);
    }

    public override void Exit()
    {
        AnimatorController.SetMoveSpeed(0f);
    }

    public override void Update()
    {
        if (TryClimb()) return;

        if (TryJump()) return;

        if (TryRunning()) return;

        if (TryIdle()) return;
    }

    public override void FixedUpdate()
    {
        View.Move(Model.MoveSpeed);

        AnimatorController.SetDirection(View.HorizontalInput);
    }
}
