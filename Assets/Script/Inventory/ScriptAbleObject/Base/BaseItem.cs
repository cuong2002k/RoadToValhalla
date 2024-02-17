using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BaseItem : ScriptableObject
{
    [SerializeField] protected string _itemName;
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected ItemType _itemType;
    [SerializeField] protected int _maxStack = 1;
    [TextArea(2, 5)][SerializeField] protected string _description;

    #region getter & setter

    public string GetItemName() { return this._itemName; }
    public Sprite GetItemIcon() { return this._icon; }
    public ItemType GetItemType() { return this._itemType; }
    public int GetMaxStacks() { return this._maxStack; }
    public string GetDescription() { return this._description; }

    #endregion

}

public enum ItemType
{
    Default,
    FoodItem,
    EquipmentItem,
    CraftItem
}