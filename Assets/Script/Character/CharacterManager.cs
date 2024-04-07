using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour, IDamage
{
    [Header("check is alive")]
    public bool IsDead = false;
    [Header("DameEffect")]
    public InstanceEffects TakeDamageEffect;

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

    public virtual IEnumerator ProcessDeathEvent()
    {
        // PlayerStatsManager.CurrentHealth.Value = 0;
        this.IsDead = true;
        yield return new WaitForSeconds(3f);
    }

    public virtual void TakeDamage(WeaponConfig weaponConfig, int physicDame, Vector3 contactPoint)
    {

    }
}
