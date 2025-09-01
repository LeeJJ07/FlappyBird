using UnityEngine;

public enum EGameFlowState
{
    Start,
    Play,
    End
}

public class GameController : MonoBehaviour
{
    [Header("Game States")]
    [SerializeField] private StartState startState;
    [SerializeField] private PlayState playState;
    [SerializeField] private EndState endState;

    private StateContext context;
    public EGameFlowState Current { get; private set; }

    private void Awake()
    {
        if (startState == null) startState = GetComponent<StartState>();
        if (playState == null) playState = GetComponent<PlayState>();
        if (endState == null) endState = GetComponent<EndState>();

        context = new StateContext();
        context.Transition(startState);
        Current = EGameFlowState.Start;
    }

    private void Update()
    {
        switch (Current)
        {
            case EGameFlowState.Start:
                if (startState.IsReadyToPlay())
                    SetState(EGameFlowState.Play);
                break;
            case EGameFlowState.Play:
                if (GameManager.Instance.IsGameOver)
                    SetState(EGameFlowState.End);
                break;
            case EGameFlowState.End:
                break;
        }
        context.CurrentState?.UpdateState();
    }

    private void SetState(EGameFlowState next)
    {
        if (Current == next) return;
        Current = next;

        switch (Current)
        {
            case EGameFlowState.Start:
                context.Transition(startState);
                break;
            case EGameFlowState.Play:
                context.Transition(playState);
                break;
            case EGameFlowState.End:
                context.Transition(endState);
                break;
        }
    }
}