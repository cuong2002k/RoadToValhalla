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

    [SerializeField] private GameObject _itemPrefabs = null;

    #region getter & setter

    public string GetItemName() => this._itemName;
    public Sprite GetItemIcon() => this._icon;
    public ItemType GetItemType() => this._itemType;
    public int GetMaxStacks() => this._maxStack;
    public string GetDescription() => this._description;
    public GameObject GetITemPrefabs() => this._itemPrefabs;
    #endregion

}

public enum ItemType
{
    Default,
    FoodItem,
    EquipmentItem,
    CraftItem
}