using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Effect", menuName = "CharacterEffect/Take Damage Effect")]
public class TakeDamageEffect : InstanceEffects
{

    public override void ProcessEffect(CharacterManager entity)
    {
        base.ProcessEffect(entity);
        if (entity.IsDead) return; // if character dead is not thing
        CaculatorDamage(entity);
        PlayVFX(targetPoint);
        PlayeSoundFX();
    }

    protected override void CaculatorDamage(CharacterManager entity)
    {
        entity.CharacterStatsManager.CurrentHealth.Value -= PhysicsDamage;
    }

    protected override void PlayeSoundFX()
    {
        WorldSFXManager.Instance.PlayRandomHitSFX();
    }

    protected override void PlayVFX(Vector3 contactPoint)
    {
        GameObject bloodVFX = Instantiate(WorldVFXManager.Instance.BloodPletterVFX, contactPoint, Quaternion.identity);
    }
}
