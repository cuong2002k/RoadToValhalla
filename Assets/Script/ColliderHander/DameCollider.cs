using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameCollider : MonoBehaviour
{
    private int _physicDame = 0;
    private Collider _colliderDamage;
    private CharacterManager _CausingCharacterManager;
    private List<CharacterManager> _characterDamaged = new List<CharacterManager>();
    private Vector3 _contactPoint;
    private WeaponConfig _weapon;

    // [Header("Test")]
    // public TakeDamageEffect TakeDamageEffect;

    private void Awake()
    {
        _colliderDamage = GetComponent<Collider>();
        _colliderDamage.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterManager characterManager = other.gameObject.GetComponentInParent<CharacterManager>();
        if (_characterDamaged.Contains(characterManager)) return;
        _characterDamaged.Add(characterManager);
        if (characterManager != null)
        {
            if (characterManager == _CausingCharacterManager) return;
            _contactPoint = other.gameObject.GetComponent<Collider>().ClosestPoint(transform.position);
            // TakeDamage(characterManager);
            characterManager.TakeDamage(_weapon, _physicDame, _contactPoint);
        }
    }

    // private void TakeDamage(CharacterManager characterManager)
    // {
    //     TakeDamageEffect.targetPoint = contactPoint;
    //     TakeDamageEffect.PhysicsDamage = physicDame;
    //     TakeDamageEffect takeDamageEffect = Instantiate(TakeDamageEffect);
    //     characterManager.CharacterEffectManager.ProcessInstanceEffect(takeDamageEffect);
    // }


    public void EnableDamage()
    {
        _colliderDamage.enabled = true;
    }

    public void DisableDamage()
    {
        _colliderDamage.enabled = false;
        _characterDamaged.Clear();
    }

    public void SetWeaponDamage(WeaponConfig weapon, CharacterManager characterManager)
    {
        _weapon = weapon;
        _physicDame = weapon.GetDamage();
        _CausingCharacterManager = characterManager;
    }

}
