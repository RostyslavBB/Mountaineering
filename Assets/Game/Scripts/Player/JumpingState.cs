public class JumpingState : PlayerState
{
    private bool _hasLeftGround;

    public JumpingState(
        PlayerModel playerModel, 
        PlayerView playerView, 
        PlayerStateMachine playerStateMachine) 
        : base(playerModel, playerView, playerStateMachine)
    {
    }

    public override void Enter()
    {
        _hasLeftGround = false;
        View.Jump(Model.JumpForce);
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        if (TryClimb()) return;

        if (!View.IsGrounded) _hasLeftGround = true;

        if (_hasLeftGround && View.IsGrounded)
        {

            if (View.HorizontalInput != 0)
            {
                if (View.IsRunning)
                {
                    StateMachine.ChangeState(
                        new RunningState(Model, View, StateMachine)
                    );
                }

                else
                {
                    StateMachine.ChangeState(
                        new WalkingState(Model, View, StateMachine)
                    );
                }
            }

            else
            {
                StateMachine.ChangeState(
                    new IdleState(Model, View, StateMachine)
                );
            }
        }
    }

    public override void FixedUpdate()
    {
        float speed = View.IsRunning
            ? Model.SprintSpeed
            : Model.MoveSpeed;

        View.Move( speed );
    }
}
