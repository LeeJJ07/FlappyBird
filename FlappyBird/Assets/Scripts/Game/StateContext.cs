public class StateContext
{
    public IState CurrentState { get; private set; }

    public void Transition(IState nextState)
    {
        CurrentState?.ExitState();
        CurrentState = nextState;
        CurrentState?.EnterState();
    }
}