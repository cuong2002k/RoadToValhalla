using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAttackState : AIBaseState
{
    public AIAttackState(AIControlManager aiControl, NavMeshAgent navMesh, Animator animator, string hasAmin) : base(aiControl, navMesh, animator, hasAmin)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _animator.applyRootMotion = false;
        _navMeshAgent.isStopped = false;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (HasAnimationFinished(_hasAmin))
        {
            _aiMachine.ChangeState(_aiControl.ChaseState);

        }
    }

    public override void OnExit()
    {
        base.OnExit();
        _animator.applyRootMotion = true;
        _navMeshAgent.isStopped = false;

    }
}
