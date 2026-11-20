public class PlayerPresenter
{
    private readonly PlayerStateMachine _stateMachine;

    public PlayerPresenter(
        PlayerView view,
        PlayerModel model,
        PlayerStateMachine stateMachine,
        PlayerAnimatorController animatorController)
    {
        _stateMachine = stateMachine;

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
    }

    public void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }
}
