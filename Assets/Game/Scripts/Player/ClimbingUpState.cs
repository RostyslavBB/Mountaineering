public class ClimbingUpState : ClimbingState
{
    public ClimbingUpState(
        PlayerModel playerModel, 
        PlayerView playerView, 
        PlayerStateMachine playerStateMachine)
        : base(playerModel, playerView, playerStateMachine)
    {
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
