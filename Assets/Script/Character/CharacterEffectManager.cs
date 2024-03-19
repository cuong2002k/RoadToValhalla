using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffectManager : MonoBehaviour
{
    protected CharacterManager characterManager;
    protected virtual void Start()
    {
        characterManager = GetComponent<CharacterManager>();
    }
    public virtual void ProcessInstanceEffect(InstanceEffects instanceEffects)
    {
        if (characterManager == null) Debug.Log("Null character Manager");
        else
            instanceEffects.ProcessEffect(characterManager);
    }

    public void PlayBloodSplatter(Transform contactPoint)
    {
        GameObject bloodVFX = Instantiate(WorldVFXManager.Instance.BloodPletterVFX, contactPoint);
    }
}
