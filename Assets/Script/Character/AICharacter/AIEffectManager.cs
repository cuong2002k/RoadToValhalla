using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEffectManager : CharacterEffectManager
{
    private CharacterAIManager _aiManager;
    private AIControlManager _aiControl;
    protected override void Awake()
    {
        base.Awake();
        _aiManager = GetComponent<CharacterAIManager>();
        _aiControl = GetComponent<AIControlManager>();
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
        }
    }


}
