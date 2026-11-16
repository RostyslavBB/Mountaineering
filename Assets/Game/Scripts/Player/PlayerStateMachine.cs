public class PlayerStateMachine
{
    public IPlayerState CurrentState {  get; private set; }

    public void ChangeState(IPlayerState newState)
    {
        if (CurrentState?.GetType() == newState.GetType()) return;

        CurrentState?.Exit();

        CurrentState = newState;

        CurrentState.Enter();
    }

    public void Update()
    {
        CurrentState?.Update();
    }

    public void FixedUpdate()
    {
        CurrentState?.FixedUpdate();
    }
}
