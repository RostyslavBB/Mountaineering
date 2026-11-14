using UnityEngine;

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
        if (!View.CanClimb)
        {
            StateMachine.ChangeState(
                new IdleState(Model, View, StateMachine)
            );

            return;
        }

        if (TryJump()) return;

        if (View.HorizontalInput != 0)
        {
            StateMachine.ChangeState(
                new WalkingState(Model, View, StateMachine)
            );

            return;
        }

        if (View.VerticalInput > 0.1f
            && StateMachine.CurrentState is not ClimbingUpState)
        {
            StateMachine.ChangeState(
                new ClimbingUpState(Model, View, StateMachine)
            );
        }
        else if (View.VerticalInput < -0.1f
            && StateMachine.CurrentState is not ClimbingDownState)
        {
            StateMachine.ChangeState(
                new ClimbingDownState(Model, View, StateMachine)
            );
        }
        else if (Mathf.Abs(View.VerticalInput) <= 0.1f
            && StateMachine.CurrentState is not ClimbingIdleState)
        {
            StateMachine.ChangeState(
                new ClimbingIdleState(Model, View, StateMachine)
            );
        }
    }

    public override void FixedUpdate()
    {
    }
}
