using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceEffects : ScriptableObject
{
    // active effect use for any character
    public virtual void ProcessEffect(CharacterManager playerManager) { }
}
