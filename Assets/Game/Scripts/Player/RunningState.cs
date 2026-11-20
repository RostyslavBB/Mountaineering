public class RunningState : PlayerState
{
    public RunningState(
        PlayerView playerView,
        PlayerModel playerModel,
        PlayerStateMachine playerStateMachine,
        PlayerAnimatorController playerAnimatorController)
        : base(playerView, playerModel, playerStateMachine, playerAnimatorController)
    {
    }

    public override void Enter()
    {
        AnimatorController.SetRunning(true);

        AnimatorController.SetMoveSpeed(1f);
    }

    public override void Exit()
    {
        AnimatorController.SetRunning(false);

        AnimatorController.SetMoveSpeed(0f);
    }

    public override void Update()
    {
        if (TryJump()) return;

        if (TryClimb()) return;

        if (TryWalk()) return;

        if (TryRunning()) return;

        if (TryIdle()) return;
    }

    public override void FixedUpdate()
    {
        View.Move(Model.SprintSpeed);

        AnimatorController.SetDirection(View.HorizontalInput);
    }
}
