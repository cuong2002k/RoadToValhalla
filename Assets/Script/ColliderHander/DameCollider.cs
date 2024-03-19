using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameCollider : MonoBehaviour
{
    public int physicDame = 0;
    private Collider _colliderDamage;
    private CharacterManager CausingCharacterManager;

    [Header("Test")]
    public TakeDamageEffect TakeDamageEffect;

    private void Start()
    {
        _colliderDamage = GetComponent<Collider>();
        _colliderDamage.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterManager characterManager = other.GetComponent<CharacterManager>();
        if (characterManager == CausingCharacterManager) return;
        if (characterManager != null)
        {
            TakeDamageEffect.PhysicsDamage = physicDame;
            characterManager.CharacterEffectManager.ProcessInstanceEffect(TakeDamageEffect);
        }
        DisableDamage();
    }

    public void EnableDamage()
    {
        _colliderDamage.enabled = true;
    }

    public void DisableDamage()
    {
        _colliderDamage.enabled = false;
    }

    public void SetWeaponDamage(WeaponConfig weapon, CharacterManager characterManager)
    {
        physicDame = weapon.GetDamage();
        CausingCharacterManager = characterManager;
    }

}
