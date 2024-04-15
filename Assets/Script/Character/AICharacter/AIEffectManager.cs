using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIEffectManager : CharacterEffectManager
{
    private CharacterAIManager _aiManager;
    private AIControlManager _aiControl;
    private NavMeshAgent _navMeshAgent;
    protected override void Awake()
    {
        base.Awake();
        _aiManager = GetComponent<CharacterAIManager>();
        _aiControl = GetComponent<AIControlManager>();
        _navMeshAgent = GetComponentInChildren<NavMeshAgent>();

    }


    protected override void Start()
    {
        base.Start();
    }

    public override void ProcessInstanceEffect(InstanceEffects instanceEffects)
    {
        base.ProcessInstanceEffect(instanceEffects);
        if (!_aiManager.IsDead)
        {
            _aiControl.AiMachine.ChangeState(_aiControl.HurtState);
            if (_aiControl.PlayerTarget == null)
            {
                _aiControl.SetTarget();
            }
        }
    }


}
