public class PlayerPresenter
{
    // Скільки часу гравець має бути в повітрі, перш ніж увімкнеться анімація падіння.
    // Прибирає мигання на нерівностях і схилах.
    private const float FallAnimationDelay = 0.1f;

    private readonly PlayerView _view;
    private readonly PlayerStateMachine _stateMachine;
    private readonly PlayerAnimatorController _animatorController;

    private float _airTime;

    public PlayerPresenter(
        PlayerView view,
        PlayerModel model,
        PlayerStateMachine stateMachine,
        PlayerAnimatorController animatorController)
    {
        _view = view;
        _stateMachine = stateMachine;
        _animatorController = animatorController;

        IdleState idleState = new(
            view,
            model,
            stateMachine,
            animatorController);

        _stateMachine.ChangeState(idleState);
    }

    public void Update()
    {
        _stateMachine.Update();

        UpdateGroundedAnimation();
    }

    public void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    private void UpdateGroundedAnimation()
    {
        _airTime = _view.IsGrounded
            ? 0f
            : _airTime + UnityEngine.Time.deltaTime;

        _animatorController.SetGrounded(_airTime < FallAnimationDelay);
    }
}
