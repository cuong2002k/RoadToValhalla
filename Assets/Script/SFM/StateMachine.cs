using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface StateMachine
{
    public void Inittialize(IState state);
    public void ChangeState(IState state);
}
