using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffectManager : MonoBehaviour
{
    protected CharacterManager characterManager;
    protected virtual void Awake()
    {

        characterManager = GetComponent<CharacterManager>();
    }
    protected virtual void Start()
    {
    }

    public virtual void ProcessInstanceEffect(InstanceEffects instanceEffects)
    {
        instanceEffects.ProcessEffect(characterManager);
    }
}
