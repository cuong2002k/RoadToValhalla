using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherableStatsManager : CharacterStatsManager
{
    private GatherableManager _gatherableManager;
    protected override void Awake()
    {
        base.Awake();
        _gatherableManager = GetComponent<GatherableManager>();
    }
    protected override void Start()
    {
        base.Start();
        CurrentHealth.Set(_maxHp.GetStatsValue());
        CurrentHealth.OnchangeValue += CheckHp;
    }

    private void CheckHp(float value)
    {
        if (value <= 0)
        {
            StartCoroutine(_gatherableManager.ProcessDeathEvent());
        }
    }
}
