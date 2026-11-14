public class ClimbingDownState : ClimbingState
{
    public ClimbingDownState(
        PlayerModel playerModel, 
        PlayerView playerView, 
        PlayerStateMachine playerStateMachine)
        : base(playerModel, playerView, playerStateMachine)
    {
    }

    public override void FixedUpdate()
    {
        View.Climb(Model.ClimbSpeed);
    }
}
