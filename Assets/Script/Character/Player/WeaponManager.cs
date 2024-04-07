using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private DameCollider _dameCollider;
    
    private void Awake()
    {
        _dameCollider = GetComponentInChildren<DameCollider>();
    }

    public void SetWeaponDamage(WeaponConfig weapon, CharacterManager characterManager)
    {
        _dameCollider.SetWeaponDamage(weapon, characterManager);
    }

    public void ActiveColliderDamage()
    {
        _dameCollider.EnableDamage();
    }

    public void DisableColliderDamage()
    {
        _dameCollider.DisableDamage();
    }

}
