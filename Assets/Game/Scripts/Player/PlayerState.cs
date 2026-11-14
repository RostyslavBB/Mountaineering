public abstract class PlayerState : IPlayerState
{
    protected readonly PlayerModel Model;
    protected readonly PlayerView View;
    protected readonly PlayerStateMachine StateMachine;

    protected PlayerState(
        PlayerModel playerModel,
        PlayerView playerView,
        PlayerStateMachine playerStateMachine)
    {
        Model = playerModel;
        View = playerView;
        StateMachine = playerStateMachine;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
    public abstract void FixedUpdate();

    protected bool TryJump()
    {
        if (!View.ConsumeJumpRequest())
            return false;

        if (!View.IsGrounded
            && StateMachine.CurrentState is not ClimbingState) return false;

        StateMachine.ChangeState(
            new JumpingState(Model, View, StateMachine)
        );


        return true;
    }

    protected bool TryClimb()
    {
        if (!View.CanClimb || View.VerticalInput == 0) return false;

        if (View.VerticalInput > 0)
        {
            StateMachine.ChangeState(
                new ClimbingUpState(Model, View, StateMachine)
            );
        }

        if (View.VerticalInput < 0)
        {
            StateMachine.ChangeState(
                new ClimbingDownState(Model, View, StateMachine)
            );
        }

        return true;
    }
}