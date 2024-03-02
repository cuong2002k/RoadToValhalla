using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandEquip
{
    LeftHand,
    RightHand,
    AbothHand
}
[CreateAssetMenu(fileName = "New Weapon Config", menuName = "Data/ItemSO/Weapon Config")]
public class WeaponConfig : BaseItem
{
    [SerializeField] private AnimatorOverrideController _animationOverride = null;
    [SerializeField] private HandEquip _handEquip;
    [SerializeField] private int _dameWeapon = 0;
    [SerializeField] private GameObject _weponObject;
    public AnimatorOverrideController GetAnimatorOverride() => _animationOverride;
    public void SpawnWeapon(Transform leftHand, Transform rightHand, Animator animator)
    {
        //destroy old Weapon
        DestroyWeapon(leftHand, rightHand);
        if (_weponObject != null)
        {
            Transform locationSpawn = GetLocationSpawn(leftHand, rightHand);
            GameObject weaponSpawn = Instantiate(this._weponObject, locationSpawn);
            weaponSpawn.name = this._itemName;
            Debug.Log("Spawn weapon");
        }

        var animatorOverride = animator.runtimeAnimatorController as AnimatorOverrideController;

        if (_animationOverride != null)
        {
            animator.runtimeAnimatorController = _animationOverride;
        }
        else if (animatorOverride != null)
        {
            animator.runtimeAnimatorController = animatorOverride;
        }

    }

    public Transform GetLocationSpawn(Transform leftHand, Transform rightHand)
    {
        Transform transformToGet = leftHand;
        if (this._handEquip == HandEquip.RightHand)
        {
            transformToGet = rightHand;
        }
        return transformToGet;
    }

    public void DestroyWeapon(Transform leftHand, Transform rightHand)
    {
        Transform oldWeapon = null;

        if (_handEquip == HandEquip.LeftHand)
        {
            oldWeapon = leftHand.Find(this._itemName);
            DestroyWeaponObject(oldWeapon);
        }
        else if (_handEquip == HandEquip.LeftHand)
        {
            oldWeapon = rightHand.Find(this._itemName);
            DestroyWeaponObject(oldWeapon);
        }
        else if (_handEquip == HandEquip.AbothHand)
        {
            oldWeapon = leftHand.Find(this._itemName);
            DestroyWeaponObject(oldWeapon);
            oldWeapon = rightHand.Find(this._itemName);
            DestroyWeaponObject(oldWeapon);
        }

    }

    private void DestroyWeaponObject(Transform oldWeapon)
    {
        if (oldWeapon != null)
        {
            Destroy(oldWeapon.gameObject);
        }
    }

    public override void Equip()
    {
        base.Equip();
        PlayerWeaponEquipment.Instance.SetWeapon(this);
    }
}
