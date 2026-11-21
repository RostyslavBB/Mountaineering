public abstract class ClimbingState : PlayerState
{
    public ClimbingState(
        PlayerView playerView,
        PlayerModel playerModel,
        PlayerStateMachine playerStateMachine,
        PlayerAnimatorController playerAnimatorController)
        : base(playerView, playerModel, playerStateMachine, playerAnimatorController)
    {
    }

    public override void Enter()
    {
        View.SetClimbing(true);

        AnimatorController.SetClimbing(true);
    }

    public override void Exit()
    {
        View.SetClimbing(false);

        AnimatorController.SetClimbing(false);
    }

    public override void Update()
    {
        if (TryJump()) return;

        if (!View.CanClimb)
            { TryIdle(); return; }

    }

    public override void FixedUpdate()
    {
    }

    // Гравець доліз до самого верху стіни й тисне вгору — вилазить на блок.
    protected bool TryClimbOntoLedge()
    {
        if (View.VerticalInput > 0.1f
            && View.CanClimb
            && View.ClimbUpSpeed == 0f
            && View.TryClimbOntoLedge())
        {
            ChangeToIdleState();

            return true;
        }

        return false;
    }

    // Гравець спустився до землі — стоїть, а не висить на стіні.
    protected bool TryStandOnGround()
    {
        if (View.IsGrounded && View.VerticalInput <= 0.1f)
        {
            ChangeToIdleState();

            return true;
        }

        return false;
    }
}
