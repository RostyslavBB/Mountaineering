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

    protected void ChangeToIdleState()
    {
        StateMachine.ChangeState(
            new IdleState(Model, View, StateMachine)
        );
    }
    
    protected void ChangeToWalkingState()
    {
        StateMachine.ChangeState(
            new WalkingState(Model, View, StateMachine)
        );
    }
    
    protected void ChangeToRunningState()
    {
        StateMachine.ChangeState(
            new RunningState(Model, View, StateMachine)
        );
    }
    
    protected void ChangeToJumpingState()
    {
        StateMachine.ChangeState(
            new JumpingState(Model, View, StateMachine)
        );
    }
    
    protected void ChangeToClimbingIdleState()
    {
        StateMachine.ChangeState(
            new ClimbingIdleState(Model, View, StateMachine)
        );
    }
    
    protected void ChangeToClimbingUpState()
    {
        StateMachine.ChangeState(
            new ClimbingUpState(Model, View, StateMachine)
        );
    }
    
    protected void ChangeToClimbingDownState()
    {
        StateMachine.ChangeState(
            new ClimbingDownState(Model, View, StateMachine)
        );
    }

    protected bool TryIdle()
    {
        if (View.HorizontalInput == 0)
        {
            ChangeToIdleState();

            return true;
        }

        return false;
    }

    protected bool TryWalk()
    {
        if (View.HorizontalInput != 0)
        {
            ChangeToWalkingState();

            return true;
        }

        return false;
    }

    protected bool TryRunning()
    {
        if (View.HorizontalInput != 0
            && View.IsRunning)
        {
            ChangeToRunningState();
            return true;
        }

        return false;
    }

    protected bool TryJump()
    {
        if (View.ConsumeJumpRequest()
            && (View.IsGrounded || View.CanClimb))
        {
            ChangeToJumpingState();

            return true;
        }

        return false;
    }

    protected bool TryClimb()
    {
        if (View.CanClimb)
        {
            if (View.VerticalInput > 0)
            {
                ChangeToClimbingUpState();

                return true;
            }

            if (View.VerticalInput < 0)
            {
                ChangeToClimbingDownState();

                return true;
            }
        }

        return false;
    }

    protected bool TryClimbUp()
    {
        if (StateMachine.CurrentState is not ClimbingUpState
            && View.VerticalInput > 0.1f)
        {
            ChangeToClimbingUpState();

            return true;
        }

        return false;
    }

    protected bool TryClimbDown()
    {
        if (StateMachine.CurrentState is not ClimbingDownState
            && View.VerticalInput < -0.1f)
        {
            ChangeToClimbingDownState();

            return true;
        }

        return false;
    }

    protected bool TryClimbIdle()
    {

        if (StateMachine.CurrentState is not ClimbingIdleState
            && UnityEngine.Mathf.Abs(View.VerticalInput) < 0.1f)
        {
            ChangeToClimbingIdleState();

            return true;
        }

        return false;
    }
}