using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStatManager : CharacterStatsManager
{
    private CharacterAIManager _aiManager;
    protected override void Awake()
    {
        base.Awake();
        _aiManager = GetComponent<CharacterAIManager>();
    }

    protected override void Start()
    {
        base.Start();
        CurrentHealth.OnchangeValue += CheckHeath;

    }

    public void CheckHeath(float Value)
    {
        if (Value <= 0)
        {
            StartCoroutine(_aiManager.ProcessDeathEvent());
        }
    }


}
