using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Effect", menuName = "CharacterEffect/Take Damage Effect")]
public class TakeDamageEffect : InstanceEffects
{
    [Header("Damage")]
    public int PhysicsDamage = 0;

    public override void ProcessEffect(CharacterManager playerManager)
    {
        base.ProcessEffect(playerManager);
        if (playerManager.IsDead) return; // if character dead is not thing
        CaculatorDamage(playerManager);
        PlayDameVFX(playerManager);
        PlayeSoundFX();
    }

    private void CaculatorDamage(CharacterManager playerManager)
    {
        playerManager.CharacterStatsManager.CurrentHealth.Value -= PhysicsDamage;
    }

    private void PlayDameVFX(CharacterManager playerManager)
    {
        playerManager.CharacterEffectManager.PlayBloodSplatter(playerManager.transform);
    }

    private void PlayeSoundFX()
    {
        WorldSFXManager.Instance.PlayRandomHitSFX();
    }
}
