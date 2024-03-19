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

    public override void Equip()
    {
        base.Equip();
        PlayerManager.Instance.PlayerWeaponEquipment.EquipWeapon(this);
    }

    public override void UnEquip()
    {
        base.UnEquip();
    }

    public GameObject GetWeaponObject() => this._weponObject;
    public int GetDamage() => _dameWeapon;
}
