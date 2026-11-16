public abstract class ClimbingState : PlayerState
{
    public ClimbingState(
        PlayerModel playerModel, 
        PlayerView playerView, 
        PlayerStateMachine playerStateMachine)
        : base(playerModel, playerView, playerStateMachine)
    {
    }

    public override void Enter()
    {
        View.SetClimbing(true);
    }

    public override void Exit()
    {
        View.SetClimbing(false);
    }

    public override void Update()
    {
        if (TryJump()) return;

        if (!View.CanClimb)
        {
            TryIdle();
            
            return;
        }

        if (TryWalk()) return;
    }

    public override void FixedUpdate()
    {
    }
}
