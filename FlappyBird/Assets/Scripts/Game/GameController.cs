using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Game States")]
    [SerializeField] private StartState startState;
    [SerializeField] private PlayState playState;
    [SerializeField] private EndState endState;

    private StateContext stateContext;
    private EState curState;
    public EState CurState { get { return curState; } }

    private void Awake()
    {
        startState = GetComponent<StartState>();
        playState = GetComponent<PlayState>();
        endState = GetComponent<EndState>();

        stateContext = new StateContext(this);
        stateContext.Transition(startState);
        curState = EState.START;
    }
    private void Update()
    {
        switch (curState)
        {
            case EState.START:
                if (startState.IsPossiblePlay())
                    UpdateState(EState.PLAY);
                break;
            case EState.PLAY:
                if (GameManager.Instance.isGameOver)
                    UpdateState(EState.END);
                break;
            case EState.END:
                break;
        }
        stateContext.CurrentState.UpdateState();
    }
    private void UpdateState(EState nextState)
    {
        if (curState == nextState)
            return;
        curState = nextState;

        switch (curState)
        {
            case EState.START:
                stateContext.Transition(startState);
                break;
            case EState.PLAY:
                stateContext.Transition(playState);
                break;
            case EState.END:
                stateContext.Transition(endState);
                break;
        }
    }
}
