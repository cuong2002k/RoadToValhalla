using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIWanderState : AIBaseState
{
    Vector3 finalPosition = Vector3.zero;
    Vector3 startPoint;
    public AIWanderState(AIControlManager aiControl, NavMeshAgent navMesh, Animator animator, string hasAmin) : base(aiControl, navMesh, animator, hasAmin)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        _navMeshAgent.destination = GetDestinationPoint();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (HasReachedDestination())
        {
            _navMeshAgent.destination = GetDestinationPoint();
        }
        _aiControl.transform.rotation = _navMeshAgent.transform.rotation;
        // else
        // {
        //     if (Vector3.Distance(finalPosition, _aiControl.transform.position) < 0.5f)
        //     {
        //         _aiMachine.ChangeState(_aiControl.IdleState);
        //     }
        // }

    }

    bool HasReachedDestination()
    {
        return !_navMeshAgent.pathPending
               && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance
               && (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f);
    }

    Vector3 GetDestinationPoint()
    {
        startPoint = _aiControl.transform.position;
        Vector3 randomDirection = Random.insideUnitSphere * 5f;
        randomDirection += startPoint;
        NavMeshHit hit;
        // NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas);
        // Vector3 finalPoint = hit.position;

        NavMesh.SamplePosition(randomDirection, out hit, 5f, 1);
        finalPosition = hit.position;
        return finalPosition;
    }
}
