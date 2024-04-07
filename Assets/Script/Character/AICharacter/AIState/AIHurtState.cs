using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIHurtState : AIBaseState
{
    public AIHurtState(AIControlManager aiControl, NavMeshAgent navMesh, Animator animator, string hasAmin) : base(aiControl, navMesh, animator, hasAmin)
    {
    }
    public override void OnEnter()
    {
        base.OnEnter();
        _animator.applyRootMotion = false;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (HasAnimationFinished(_hasAmin))
        {
            _aiMachine.ChangeState(_aiControl.IdleState);
        }
    }

    public override void OnExit()
    {
        base.OnExit(); _animator.applyRootMotion = true;
    }



}
