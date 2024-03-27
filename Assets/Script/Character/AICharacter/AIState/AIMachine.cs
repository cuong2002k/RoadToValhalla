using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIMachine : StateMachine
{
    public IState currentState { get; private set; }

    public void Inittialize(IState state)
    {
        if (state != null)
        {
            currentState = state;
            currentState.OnEnter();
        }
    }
    public void ChangeState(IState state)
    {
        if (state != null)
        {
            currentState.OnExit();
            currentState = state;
            currentState.OnEnter();
        }
    }
}
