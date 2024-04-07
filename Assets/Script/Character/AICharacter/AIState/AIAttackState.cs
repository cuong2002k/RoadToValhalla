using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAttackState : AIBaseState
{
    public AIAttackState(AIControlManager aiControl, NavMeshAgent navMesh, Animator animator, string hasAmin) : base(aiControl, navMesh, animator, hasAmin)
    {
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (HasAnimationFinished(_hasAmin))
        {
            _aiMachine.ChangeState(_aiControl.ChaseState);
        }
    }
}
