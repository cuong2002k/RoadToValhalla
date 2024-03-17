using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : CharacterEffectManager
{
    private PlayerManager _playerManager;
    [Header("Test => delete on time")]
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
        _playerManager.PlayerController.playerMachine.ChangeState(_playerManager.PlayerController.HurtState);
        instanceEffects.ProcessEffect(_playerManager);
    }

    public void PlayBloodSplatter(Transform contactPoint)
    {
        GameObject bloodVFX = Instantiate(WorldVFXManager.Instance.BloodPletterVFX, contactPoint);
    }
}
