using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiDeathState : AIBaseState
{
    public AiDeathState(AIControlManager aiControl, NavMeshAgent navMesh, Animator animator, string hasAmin) : base(aiControl, navMesh, animator, hasAmin)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }
}
