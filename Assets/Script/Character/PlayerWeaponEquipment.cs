using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponEquipment : MonoBehaviour
{
    #region 
    public static PlayerWeaponEquipment Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion


    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;
    public Transform GetLeftHand() => this._leftHand;
    public Transform GetRightHand() => this._rightHand;
    [SerializeField] private WeaponConfig _leftHandWeapon;
    [SerializeField] private WeaponConfig _rightHandWeapon;
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

    public void SetWeapon(WeaponConfig weaponEquip)
    {
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

    public void UpdateWeapon(WeaponConfig weapon)
    {
        SpawnWeapon(_leftHand, _rightHand, _playerAnimator, weapon);
    }

    public void UnEquipWeapon(WeaponConfig weapon)
    {
        weapon.DestroyWeapon(_leftHand, _rightHand);
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
        return (_leftHand != null) && (_leftHand.childCount > 0);
    }
    public bool HasItemRightHand()
    {
        return (_rightHand != null) && (_rightHand.childCount > 0);
    }


}
