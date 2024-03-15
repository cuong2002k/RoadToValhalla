using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponEquipment : MonoBehaviour
{
    #region two hand player
    [SerializeField] private Transform _leftHandTranform;
    [SerializeField] private Transform _rightHandTranform;
    public Transform GetLeftHand() => this._leftHandTranform;
    public Transform GetRightHand() => this._rightHandTranform;
    [SerializeField] private WeaponConfig _leftHandWeapon;
    [SerializeField] private WeaponConfig _rightHandWeapon;

    #endregion

    private Animator _playerAnimator;
    [SerializeField] private AnimatorOverrideController _defaultAnimtor;

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void SpawnWeapon(Transform leftHand, Transform rightHand, Animator playerAnimator, WeaponConfig weapon)
    {
        weapon.SpawnWeapon(leftHand, rightHand, playerAnimator);
    }

    public void EquipWeapon(WeaponConfig weaponEquip)
    {
        if (weaponEquip == null)
        {
            Debug.LogError("weapon is not null");
            return;
        }
        // check hand weapon
        if (weaponEquip.GetHandEquip() == HandEquip.LeftHand)
        {
            _leftHandWeapon = weaponEquip;
        }
        else if (weaponEquip.GetHandEquip() == HandEquip.RightHand)
        {
            _rightHandWeapon = weaponEquip;
        }
        UpdateWeapon(weaponEquip);
    }
    // update weapon in hand
    private void UpdateWeapon(WeaponConfig weapon)
    {
        SpawnWeapon(_leftHandTranform, _rightHandTranform, _playerAnimator, weapon);
    }

    public void UnEquipWeapon(WeaponConfig weapon)
    {
        weapon.DestroyWeapon(_leftHandTranform, _rightHandTranform);
        if (weapon.GetHandEquip() == HandEquip.LeftHand) _leftHandWeapon = null;
        else if (weapon.GetHandEquip() == HandEquip.RightHand) _rightHandWeapon = null;
        else
        {
            _leftHandWeapon = null;
            _rightHandWeapon = null;
        }
        _playerAnimator.runtimeAnimatorController = _defaultAnimtor;
    }

    public bool HasItemLeftHand()
    {
        return HasContainsItem(_leftHandTranform);
    }

    public bool HasItemRightHand()
    {
        return HasContainsItem(_rightHandTranform);
    }

    public bool HasContainsItem(Transform transform)
    {
        return (transform != null) && (transform.childCount > 0);
    }
}
