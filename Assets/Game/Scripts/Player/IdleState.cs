public class IdleState : PlayerState
{
    public IdleState(
        PlayerModel playerModel,
        PlayerView playerView,
        PlayerStateMachine playerStateMachine)
        : base(playerModel, playerView, playerStateMachine)
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
    }

    public override void FixedUpdate()
    {
    }

}
