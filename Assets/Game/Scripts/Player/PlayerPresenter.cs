public class PlayerPresenter
{
    private readonly PlayerStateMachine _playerStateMachine;

    public PlayerPresenter(
        PlayerModel model,
        PlayerView view,
        PlayerStateMachine playerStateMachine)
    {
        _playerStateMachine = playerStateMachine;

        IdleState idleState = new(
            model,
            view,
            playerStateMachine
        );

        _playerStateMachine.ChangeState(idleState);
    }

    public void Update()
    {
        _playerStateMachine.Update();
    }

    public void FixedUpdate()
    {
        _playerStateMachine.FixedUpdate();
    }
}
