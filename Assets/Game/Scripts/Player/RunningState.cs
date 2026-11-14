public class RunningState : PlayerState
{
    public RunningState(
        PlayerModel playerModel, 
        PlayerView playerView, 
        PlayerStateMachine playerStateMachine)
        : base(playerModel, playerView, playerStateMachine)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        if (TryClimb()) return;

        if (TryJump()) return;

        if (View.HorizontalInput == 0)
        {
            StateMachine.ChangeState(
                new IdleState(Model, View, StateMachine)
            );

            return;
        }

        if (!View.IsRunning)
        {
            StateMachine.ChangeState(
                new WalkingState(Model, View, StateMachine)
            );

            return;
        }
    }
    public override void FixedUpdate()
    {
        View.Move(Model.SprintSpeed);
    }

}
