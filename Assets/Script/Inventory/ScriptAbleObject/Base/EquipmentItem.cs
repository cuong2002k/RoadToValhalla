using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum EquipmentType // slot equipment
{
    Helmet,
    ChestArmor,
    Shoes
}

[CreateAssetMenu(fileName = "New EquipmentItem", menuName = "Data/ItemSO/Equipment Item")]
public class EquipmentItem : BaseItem
{
    [SerializeField] private EquipmentType _equipmentType;
    [SerializeField] private int _attackModifier;
    [SerializeField] private int _armorModifier;
    [SerializeField] private SkinnedMeshRenderer _equipmentMesh;

    #region getter & setter
    public EquipmentType GetEquipmentType() => this._equipmentType;
    public SkinnedMeshRenderer GetEquipmentMesh() => this._equipmentMesh;
    public int GetAttackModified() => this._attackModifier;

    public int GetArmorModifier() => this._armorModifier;

    #endregion

    public override void Equip()
    {
        base.Equip();
        PlayerManager.Instance.EquipmentManager.Equip(this);
    }

    public override void UnEquip()
    {
        base.UnEquip();
        InventoryController inventory = PlayerUIManager.Instance.InventoryController;
        inventory.AddItem(new ItemStack(this, 1));
    }

}
