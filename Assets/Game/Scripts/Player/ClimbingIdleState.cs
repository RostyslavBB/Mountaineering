public class ClimbingIdleState : ClimbingState
{
    public ClimbingIdleState(
        PlayerModel playerModel, 
        PlayerView playerView, 
        PlayerStateMachine playerStateMachine)
        : base(playerModel, playerView, playerStateMachine)
    {
    }

    public override void Update()
    {
        if (TryClimbUp()) return;

        if (TryClimbDown()) return;

        base.Update();
    }
}
