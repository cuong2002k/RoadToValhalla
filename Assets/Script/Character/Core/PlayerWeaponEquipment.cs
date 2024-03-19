using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponEquipment : MonoBehaviour
{
    private PlayerManager _playerManager;
    private Animator _playerAnimator;

    #region two hand player
    [SerializeField] private WeaponInstance _leftHandInstance;
    [SerializeField] private WeaponInstance _rightHandInstance;
    [SerializeField] private WeaponConfig _leftHandWeapon;
    [SerializeField] private WeaponConfig _rightHandWeapon;
    [SerializeField] private WeaponConfig _defaultWeapon;

    public WeaponConfig LeftHandWeapon() => _leftHandWeapon != null ? _leftHandWeapon : _defaultWeapon;

    public WeaponConfig RightHandWeapon() => _rightHandWeapon != null ? _rightHandWeapon : _defaultWeapon;

    public WeaponInstance RightHandWeaponInstance
    {
        get
        {
            if (_rightHandInstance == null)
            {
                EquipWeapon(_defaultWeapon);
            }
            return _rightHandInstance;
        }
    }

    public WeaponInstance LeftHandWeaponInstance
    {
        get
        {
            if (_leftHandInstance == null)
            {
                EquipWeapon(_defaultWeapon);
            }
            return _leftHandInstance;
        }
    }
    #endregion


    private void Awake()
    {
        InitWeaponInstance();
    }

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerManager = GetComponent<PlayerManager>();
        EquipWeapon(_defaultWeapon);
    }

    public void InitWeaponInstance()
    {
        foreach (WeaponInstance weaponInstance in GetComponentsInChildren<WeaponInstance>())
        {
            if (weaponInstance.handEquip == HandEquip.LeftHand)
            {
                _leftHandInstance = weaponInstance;
            }
            else if (weaponInstance.handEquip == HandEquip.RightHand)
            {
                _rightHandInstance = weaponInstance;
            }
        }
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
            _leftHandInstance.UploadWeapon(LeftHandWeapon(), _playerManager);
        }
        else if (weaponEquip.GetHandEquip() == HandEquip.RightHand)
        {
            _rightHandWeapon = weaponEquip;
            _rightHandInstance.UploadWeapon(RightHandWeapon(), _playerManager);
        }
        else
        {
            _leftHandWeapon = weaponEquip;
            _rightHandWeapon = weaponEquip;
            _leftHandInstance.UploadWeapon(LeftHandWeapon(), _playerManager);
            _rightHandInstance.UploadWeapon(RightHandWeapon(), _playerManager);
        }
        if (RightHandWeapon().GetAnimatorOverride() != null)
        {
            _playerAnimator.runtimeAnimatorController = RightHandWeapon().GetAnimatorOverride();
        }

    }

    public bool HasItemLeftHand()
    {
        return HasContainsItem(_leftHandInstance);
    }

    public bool HasItemRightHand()
    {
        return HasContainsItem(_rightHandInstance);
    }

    public bool HasContainsItem(WeaponInstance weaponInstance)
    {
        return (transform != null) && (weaponInstance.transform.childCount > 0);
    }

    public void UnEquipWeapon(WeaponConfig weaponConfig)
    {
        if (weaponConfig.GetHandEquip() == HandEquip.LeftHand)
        {
            UnLoadLeftHand();
            _leftHandInstance.UploadWeapon(_defaultWeapon, _playerManager);
        }
        else if (weaponConfig.GetHandEquip() == HandEquip.RightHand)
        {
            UnloadRightHand();
            _rightHandInstance.UploadWeapon(_defaultWeapon, _playerManager);
        }
        else
        {
            UnLoadLeftHand();
            UnloadRightHand();
            _leftHandInstance.UploadWeapon(_defaultWeapon, _playerManager);
            _rightHandInstance.UploadWeapon(_defaultWeapon, _playerManager);
        }

        _playerAnimator.runtimeAnimatorController = RightHandWeapon().GetAnimatorOverride();

    }

    private void UnLoadLeftHand()
    {
        _leftHandWeapon = _defaultWeapon;
        _leftHandInstance.UnloadWeapon();
    }

    private void UnloadRightHand()
    {
        _rightHandWeapon = _defaultWeapon;
        _rightHandInstance.UnloadWeapon();
    }
}
