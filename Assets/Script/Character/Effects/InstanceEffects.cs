using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceEffects : ScriptableObject
{
    [Header("Damage")]
    public int PhysicsDamage = 0;
    public Vector3 targetPoint;
    // active effect use for any character
    public virtual void ProcessEffect(CharacterManager entity) { }
    protected virtual void CaculatorDamage(CharacterManager entity)
    {

    }

    protected virtual void PlayeSoundFX()
    {

    }

    protected virtual void PlayVFX(Vector3 contactPoint)
    {

    }
}
