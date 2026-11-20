public class IdleState : PlayerState
{
    public IdleState(
        PlayerView playerView,
        PlayerModel playerModel,
        PlayerStateMachine playerStateMachine,
        PlayerAnimatorController playerAnimatorController)
        : base(playerView, playerModel, playerStateMachine, playerAnimatorController)
    {
    }

    public override void Enter()
    {
        View.StopMovement();
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        if (TryClimb()) return;

        if (TryJump()) return;

        if (TryWalk()) return;

        if (TryRunning()) return;
    }

    public override void FixedUpdate()
    {
    }

}
