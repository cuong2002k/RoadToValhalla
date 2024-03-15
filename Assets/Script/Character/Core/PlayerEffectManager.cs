using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : CharacterEffectManager
{
    private PlayerManager _playerManager;
    public InstanceEffects test;
    public bool testEffects;

    private void Start()
    {
        _playerManager = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (testEffects)
        {
            testEffects = false;
            TakeDamageEffect damageEffect = Instantiate(test) as TakeDamageEffect;
            ProcessInstanceEffect(damageEffect);
        }
    }

    public override void ProcessInstanceEffect(InstanceEffects instanceEffects)
    {
        base.ProcessInstanceEffect(instanceEffects);
        instanceEffects.ProcessEffect(_playerManager);
    }
}
