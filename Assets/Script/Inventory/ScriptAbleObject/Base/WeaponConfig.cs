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
    public HandEquip GetHandEquip() => this._handEquip;
    [SerializeField] private AudioClip[] _attackSound;
    public AudioClip GetAttackSound()
    {
        if (_attackSound.Length > 0)
        {
            int randomValue = Random.Range(0, _attackSound.Length);
            return _attackSound[randomValue];
        }
        return null;
    }
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

            DestroyWeaponObject(leftHand);
        }
        else if (_handEquip == HandEquip.RightHand)
        {
            oldWeapon = rightHand.Find(this._itemName);
            DestroyWeaponObject(rightHand);
        }
        else if (_handEquip == HandEquip.AbothHand)
        {
            DestroyWeaponObject(leftHand);
            DestroyWeaponObject(rightHand);
        }

    }

    private void DestroyWeaponObject(Transform hand)
    {
        for (int i = 0; i < hand.childCount; i++)
        {
            Transform weapon = hand.transform.GetChild(i);
            if (weapon != null)
            {
                Destroy(weapon.gameObject);
            }
        }

    }

    public override void Equip()
    {
        base.Equip();
        PlayerManager.Instance.PlayerWeaponEquipment.EquipWeapon(this);
    }

    public override void UnEquip()
    {
        base.UnEquip();
        PlayerManager.Instance.PlayerWeaponEquipment.UnEquipWeapon(this);
    }

}
