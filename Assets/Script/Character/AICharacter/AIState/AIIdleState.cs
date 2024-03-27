using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIIdleState : AIBaseState
{
    private float _idleTimer = 4f;
    public AIIdleState(AIControlManager aiControl, NavMeshAgent navMesh, Animator animator, string hasAmin) : base(aiControl, navMesh, animator, hasAmin)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // if (Time.time >= _startTime + _idleTimer)
        // {
        //     _aiMachine.ChangeState(_aiControl.WanderState);
        // }
    }


}
