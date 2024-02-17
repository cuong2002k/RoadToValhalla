using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IState
{
    public abstract void OnEnter();
    public abstract void DoCheck();
    public abstract void LogicUpdate();
    public abstract void PhysicUpdate();
    public abstract void OnExit();
}
