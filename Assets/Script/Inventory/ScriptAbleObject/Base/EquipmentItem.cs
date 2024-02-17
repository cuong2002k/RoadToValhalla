using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EquipmentType // slot equipment
{
    Weapon,
    Shield,
    Helmet,
    ChestArmor,
    Shoes,
}

[CreateAssetMenu(fileName = "New EquipmentItem", menuName = "Data/ItemSO/Equipment Item")]
public class EquipmentItem : BaseItem
{
    [SerializeField] private EquipmentType _equipmentType;
    [SerializeField] private int _attackModifier;
    [SerializeField] private int _armorModifier;

    #region getter & setter

    public EquipmentType GetEquipmentType() { return this._equipmentType; }

    #endregion



}
