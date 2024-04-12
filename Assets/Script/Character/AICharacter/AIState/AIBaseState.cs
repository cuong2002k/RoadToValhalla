using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIBaseState : IState
{
    protected AIControlManager _aiControl;
    protected NavMeshAgent _navMeshAgent;
    protected Animator _animator;
    protected AIMachine _aiMachine;
    protected string _hasAmin;

    protected float _crossDuration;
    protected float _startTime;

    public AIBaseState(AIControlManager aiControl, NavMeshAgent navMesh, Animator animator, string hasAmin)
    {
        this._aiControl = aiControl;
        this._navMeshAgent = navMesh;
        this._animator = animator;
        this._hasAmin = hasAmin;
        this._aiMachine = _aiControl.AiMachine;
    }
    public override void OnEnter()
    {
        DoCheck();
        _animator.CrossFade(_hasAmin, _crossDuration);
        _startTime = Time.time;
        Debug.Log(_hasAmin);
    }
    public override void DoCheck() { }

    public override void LogicUpdate()
    {
        if (CanAttack() && _aiControl.AttackCountDown.Timer <= 0)
        {
            _aiMachine.ChangeState(_aiControl.AttackState);
            _aiControl.AttackCountDown.Reset();
        }
    }

    public override void PhysicUpdate() => DoCheck();

    public override void OnExit()
    {
        _startTime = 0f;
    }

    protected bool HasAnimationFinished()
    {
        Animator animator = _animator;
        AnimatorStateInfo animStateInfo;
        float NTime;
        bool animationFinished = false; ;
        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        NTime = animStateInfo.normalizedTime;
        if (NTime > 1.0f) animationFinished = true;
        return animationFinished;
    }

    protected bool HasAnimationFinished(string animationName)
    {
        // Check if the specified animation has completed
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1.0f;
    }

    public bool CanAttack()
    {
        if (_aiControl.PlayerTarget == null) return false;
        float remainingDistance = Vector3.Distance(_aiControl.PlayerTarget.position, _aiControl.transform.position);
        if (remainingDistance <= _aiControl.attackRange)
        {
            return !_aiControl.characterAIManager.IsDead;
        }
        return false;
    }
}
