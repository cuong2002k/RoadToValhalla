using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIChaseState : AIBaseState
{
    public AIChaseState(AIControlManager aiControl, NavMeshAgent navMesh, Animator animator, string hasAmin) : base(aiControl, navMesh, animator, hasAmin)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (Time.time >= _startTime + _aiControl.timeFocus && !_aiControl.lookPlayer)
        {
            _aiControl.AiMachine.ChangeState(_aiControl.IdleState);
            _aiControl.PlayerTarget = null;
        }
        else if (_aiControl.lookPlayer)
        {
            _startTime = Time.time;
        }

        if (_aiControl.isRunning && _aiControl.PlayerTarget != null)
        {
            _navMeshAgent.SetDestination(_aiControl.PlayerTarget.position);
        }
        else
        {
            _aiControl.AiMachine.ChangeState(_aiControl.IdleState);
        }


        _aiControl.transform.rotation = _navMeshAgent.transform.rotation;
        _navMeshAgent.transform.localPosition = Vector3.zero;
        _navMeshAgent.transform.localRotation = Quaternion.identity;
    }




}
