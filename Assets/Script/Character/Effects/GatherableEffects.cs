using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Damage Effect", menuName = "CharacterEffect/GatherableEffects Effect")]
public class GatherableEffects : InstanceEffects
{
    public override void ProcessEffect(CharacterManager playerManager)
    {
        base.ProcessEffect(playerManager);

        if (playerManager != null)
        {
            if (playerManager.IsDead) return;
            CaculatorDamage(playerManager);
            PlayeSoundFX();
            PlayVFX(targetPoint);
        }
    }

    protected override void CaculatorDamage(CharacterManager playerManager)
    {
        playerManager.CharacterStatsManager.CurrentHealth.Value -= PhysicsDamage;
    }

    protected override void PlayeSoundFX()
    {
        WorldSFXManager.Instance.PlayRandomHitSFX();
    }

    protected override void PlayVFX(Vector3 contactPoint)
    {
        GameObject bloodVFX = Instantiate(WorldVFXManager.Instance.ChopTreeVFX, contactPoint, Quaternion.identity);
    }
}
