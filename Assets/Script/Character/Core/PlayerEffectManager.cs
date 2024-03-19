using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : CharacterEffectManager
{
    [Header("Test => delete on time")]
    private PlayerManager _playerManager;
    public InstanceEffects test;
    public bool testEffects;
    protected override void Start()
    {
        base.Start();
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


    // process effect
    public override void ProcessInstanceEffect(InstanceEffects instanceEffects)
    {
        base.ProcessInstanceEffect(instanceEffects);
        if (!_playerManager.IsDead)
            _playerManager.PlayerController.playerMachine.ChangeState(_playerManager.PlayerController.HurtState);
    }


}
