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
        int finalDamage = PhysicsDamage - entity.CharacterStatsManager.Defense.GetStatsValue();
        if (finalDamage <= 0) finalDamage = 1;
        else finalDamage = Random.Range(Mathf.Max(0, finalDamage - 5), finalDamage);
        entity.CharacterStatsManager.CurrentHealth.Value -= finalDamage;
        WorldVFXManager.Instance.CreatePopupDamage(targetPoint, finalDamage);
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
