public abstract class ClimbingState : PlayerState
{
    public ClimbingState(
        PlayerView playerView,
        PlayerModel playerModel,
        PlayerStateMachine playerStateMachine,
        PlayerAnimatorController playerAnimatorController)
        : base(playerView, playerModel, playerStateMachine, playerAnimatorController)
    {
    }

    public override void Enter()
    {
        View.SetClimbing(true);

        AnimatorController.SetClimbing(true);
    }

    public override void Exit()
    {
        View.SetClimbing(false);

        AnimatorController.SetClimbing(false);
    }

    public override void Update()
    {
        if (TryJump()) return;

        if (!View.CanClimb)
            { TryIdle(); return; }

    }

    public override void FixedUpdate()
    {
    }
}
