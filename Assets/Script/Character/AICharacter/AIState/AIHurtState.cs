using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIHurtState : AIBaseState
{
    public AIHurtState(AIControlManager aiControl, NavMeshAgent navMesh, Animator animator, string hasAmin) : base(aiControl, navMesh, animator, hasAmin)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log(HasAnimationFinished(_hasAmin));
        if (HasAnimationFinished(_hasAmin))
        {
            _aiMachine.ChangeState(_aiControl.IdleState);
        }
    }

    protected bool HasAnimationFinished(string animationName)
    {
        // Check if the specified animation has completed
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1.0f;
    }

}
