public class ClimbingUpState : ClimbingState
{
    public ClimbingUpState(
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

        AnimatorController.SetClimbY(1f);
    }

    public override void Exit()
    {
        base.Exit();

        AnimatorController.SetClimbY(0f);
    }

    public override void Update()
    {
        if (TryClimbDown()) return;

        if (TryClimbIdle()) return;

        base.Update();
    }

    public override void FixedUpdate()
    {
        View.Climb(Model.ClimbSpeed);
    }
}
