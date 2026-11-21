public class ClimbingIdleState : ClimbingState
{
    public ClimbingIdleState(
        PlayerView playerView,
        PlayerModel playerModel,
        PlayerStateMachine playerStateMachine,
        PlayerAnimatorController playerAnimatorController)
        : base(playerView, playerModel, playerStateMachine, playerAnimatorController)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AnimatorController.SetClimbY(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        if (TryClimbOntoLedge()) return;

        if (TryStandOnGround()) return;

        if (TryClimbUp()) return;

        if (TryClimbDown()) return;

        base.Update();
    }
}
