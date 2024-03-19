using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInstance : MonoBehaviour
{
    private GameObject _weaponInstance;
    [SerializeField] private WeaponManager _weaponManager;

    public HandEquip handEquip;

    public void UploadWeapon(WeaponConfig weapon, CharacterManager characterManager)
    {
        UnloadWeapon();
        _weaponInstance = Instantiate(weapon.GetWeaponObject(), this.transform);
        _weaponInstance.transform.SetParent(this.transform);
        _weaponManager = _weaponInstance.GetComponent<WeaponManager>();
        if (_weaponManager != null)
        {
            _weaponManager.SetWeaponDamage(weapon, characterManager);
        }
    }

    public void UnloadWeapon()
    {
        if (this._weaponInstance != null)
        {
            Destroy(_weaponInstance);
            _weaponManager = null;
        }
    }

    public void ActiveColliderDamage()
    {
        _weaponManager.ActiveColliderDamage();
    }

    public void DisableColliderDamage()
    {
        _weaponManager.DisableColliderDamage();
    }


}
