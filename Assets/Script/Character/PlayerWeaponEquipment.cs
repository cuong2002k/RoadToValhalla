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
    [SerializeField] private WeaponConfig _defaultWeapon;
    private Animator _playerAnimator;

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void SpawnWeapon(Transform leftHand, Transform rightHand, Animator playerAnimator)
    {
        _defaultWeapon.SpawnWeapon(leftHand, rightHand, playerAnimator);
    }

    public void SetWeapon(WeaponConfig weaponEquip)
    {
        this._defaultWeapon = weaponEquip;
        UpdateWeapon();
    }

    public void UpdateWeapon()
    {
        SpawnWeapon(_leftHand, _rightHand, _playerAnimator);

    }



}
