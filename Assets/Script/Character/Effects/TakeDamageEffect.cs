using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Effect", menuName = "CharacterEffect/Take Damage Effect")]
public class TakeDamageEffect : InstanceEffects
{
    [Header("Damage")]
    public int PhysicsDamage = 0;

    public override void ProcessEffect(PlayerManager playerManager)
    {
        base.ProcessEffect(playerManager);

        TakeDamage(playerManager);
    }

    private void TakeDamage(PlayerManager playerManager)
    {
        if (playerManager.IsDead) return;
        playerManager.PlayerStatsManager.CurrentHealth.Value -= PhysicsDamage;
    }
}