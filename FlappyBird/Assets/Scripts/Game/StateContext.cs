using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EState
{
    START,
    PLAY,
    BUFF,
    END
}
public class StateContext
{

    public IState CurrentState { get; set; }
    private readonly GameController controller;
    
    public StateContext(GameController controller)
    {
        this.controller = controller;
    }
    public void Transition(IState gameState)
    {
        if (CurrentState != null)
            CurrentState.ExitState();
        CurrentState = gameState;

        CurrentState.EnterState();
    }
}
