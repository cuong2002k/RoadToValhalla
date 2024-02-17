using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMachine : StateMachine
{
    public IState CurrentState { get; private set; }

    public void Inittialize(IState state)
    {
        this.CurrentState = state;
        this.CurrentState.OnEnter();
    }


    public void ChangeState(IState state)
    {
        if (state != this.CurrentState)
        {
            CurrentState.OnExit();
            CurrentState = state;
            CurrentState.OnEnter();
        }
    }
}
