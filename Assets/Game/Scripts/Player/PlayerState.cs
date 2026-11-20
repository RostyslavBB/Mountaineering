public abstract class PlayerState : IPlayerState
{
    protected readonly PlayerView View;
    protected readonly PlayerModel Model;
    protected readonly PlayerStateMachine StateMachine;
    protected readonly PlayerAnimatorController AnimatorController;

    protected PlayerState(
        PlayerView playerView,
        PlayerModel playerModel,
        PlayerStateMachine playerStateMachine,
        PlayerAnimatorController playerAnimatorController)
    {
        View = playerView;
        Model = playerModel;
        StateMachine = playerStateMachine;
        AnimatorController = playerAnimatorController;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
    public abstract void FixedUpdate();

    protected void ChangeToIdleState()
    {
        StateMachine.ChangeState(
            new IdleState(View, Model, StateMachine, AnimatorController)
        );
    }
    
    protected void ChangeToWalkingState()
    {
        StateMachine.ChangeState(
            new WalkingState(View, Model, StateMachine, AnimatorController)
        );
    }
    
    protected void ChangeToRunningState()
    {
        StateMachine.ChangeState(
            new RunningState(View, Model, StateMachine, AnimatorController)
        );
    }
    
    protected void ChangeToJumpingState()
    {
        StateMachine.ChangeState(
            new JumpingState(View, Model, StateMachine, AnimatorController)
        );
    }
    
    protected void ChangeToClimbingIdleState()
    {
        StateMachine.ChangeState(
            new ClimbingIdleState(View, Model, StateMachine, AnimatorController)
        );
    }
    
    protected void ChangeToClimbingUpState()
    {
        StateMachine.ChangeState(
            new ClimbingUpState(View, Model, StateMachine, AnimatorController)
        );
    }
    
    protected void ChangeToClimbingDownState()
    {
        StateMachine.ChangeState(
            new ClimbingDownState(View, Model, StateMachine, AnimatorController)
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
        if (View.HorizontalInput != 0
            && !View.IsRunning)
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
            if (TryClimbUp()) return true;

            if (TryClimbDown()) return true;
        }

        return false;
    }

    protected bool TryClimbUp()
    {
        if (StateMachine.CurrentState is not ClimbingUpState
            && View.VerticalInput > 0.1f
            && View.ClimbUpSpeed > 0)
        {
            ChangeToClimbingUpState();

            return true;
        }

        return false;
    }

    protected bool TryClimbDown()
    {
        if (StateMachine.CurrentState is not ClimbingDownState
            && View.VerticalInput < -0.1f
            && !View.IsGrounded)
        {
            ChangeToClimbingDownState();

            return true;
        }

        return false;
    }

    protected bool TryClimbIdle()
    {

        if (StateMachine.CurrentState is not ClimbingIdleState
            && (UnityEngine.Mathf.Abs(View.VerticalInput) < 0.1f
            || (View.ClimbUpSpeed == 0 || View.IsGrounded)))
        {
            ChangeToClimbingIdleState();

            return true;
        }

        return false;
    }
}