using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("check is alive")]
    public bool IsDead = false;
    [Header("component")]
    [HideInInspector] public CharacterEffectManager CharacterEffectManager;
    [HideInInspector] public CharacterStatsManager CharacterStatsManager;

    protected virtual void Awake() { }
    protected virtual void Start()
    {
        CharacterEffectManager = GetComponent<CharacterEffectManager>();
        CharacterStatsManager = GetComponent<CharacterStatsManager>();
    }
    protected virtual void Update() { }
    protected virtual void LateUpdate() { }

}
