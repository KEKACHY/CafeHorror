public class StateMachine
{
    private IState _currentState;
    public event System.Action<string> OnStateChanged;
    public void ChangeState(IState newState)
    {
        if(_currentState == newState)
            return;
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();

        OnStateChanged?.Invoke(_currentState.GetType().Name);
    }
    public void Update()
    {
        _currentState?.Update();
    }
}