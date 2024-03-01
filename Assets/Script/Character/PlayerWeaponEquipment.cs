using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponEquipment : MonoBehaviour
{
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private WeaponConfig _defaultWeapon;
    [SerializeField] private Animator _playerAnimator;

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
        SpawnWeapon(_leftHand, _rightHand, _playerAnimator);
    }

    private void SpawnWeapon(Transform leftHand, Transform rightHand, Animator playerAnimator)
    {
        _defaultWeapon.SpawnWeapon(leftHand, rightHand, playerAnimator);

    }





}
