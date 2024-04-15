using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStack
{
    [field: SerializeField] public string Id;
    [SerializeField] private BaseItem _item = null;
    [SerializeField] private int _stack = 0;
    [SerializeField] private bool _isActive = false;
    #region Constructer
    public ItemStack()
    {
        this._item = null;
        this._stack = 0;
    }

    public ItemStack(BaseItem item, int stack)
    {
        this._item = item;
        this._stack = stack;
        this._isActive = false;
        this.Id = item.ID;
    }

    public ItemStack(BaseItem item, int stack, bool isActive)
    {
        this._item = item;
        this._stack = stack;
        this._isActive = isActive;
        this.Id = item.ID;
    }
    #endregion

    #region Get & Set
    public BaseItem GetItem() => this._item;
    public int GetStack() => this._stack;
    public bool GetActive() => this._isActive;
    public bool SetActive(bool value) => this._isActive = value;
    public void SetItemStack(ItemStack item)
    {
        this._item = item.GetItem();
        this._stack = item.GetStack();
        this._isActive = item.GetActive();
        this.Id = item.Id;
    }

    public void SetItemStack(BaseItem item, int stack)
    {
        this._item = item;
        this._stack = stack;
        this.Id = item.ID;
    }

    public void SetStack(int stack)
    {
        this._stack = stack;
        if (this._stack == 0) this.Clear();
    }

    public void SetItem(BaseItem item)
    {
        this._item = item;
        this.Id = item.ID;
    }

    public int GetStackAvailable()
    {
        return this._item.GetMaxStacks() - this._stack;
    }
    #endregion


    #region check
    public bool IsEmpty()
    {
        return this._stack < 1;
    }

    public bool IsItemEqual(ItemStack item)
    {
        return (item != null) && (item.GetItem().Equals(this._item));
    }

    #endregion

    #region logic
    public int AddStack(int stackToAdd)
    {
        if (this.GetStackAvailable() >= stackToAdd)
        {
            this._stack += stackToAdd;
            return 0;
        }
        else
        {
            stackToAdd -= GetStackAvailable();
            this._stack = this._item.GetMaxStacks();
            return stackToAdd;
        }
    }

    public void DecreaseStack(int stackToDecrease)
    {
        this._stack -= stackToDecrease;
        if (this._stack < 1) Clear();
    }

    public ItemStack Copy(int stack)
    {
        return new ItemStack(this._item, stack);
    }

    public ItemStack SplitStack(int stackToSplit)
    {
        int stackSplit = Mathf.Min(this._stack, stackToSplit);
        ItemStack itemToSplit = this.Copy(stackSplit);
        itemToSplit.SetStack(stackSplit);
        return itemToSplit;
    }

    public void Clear()
    {
        this._item = null;
        this._stack = 0;
        this._isActive = false;
        this.Id = "";
    }

    public void DropItem(ItemStack itemToDrop, Transform dropLocation, int stack)
    {
        ItemPickUp itemPickUp = Object.Instantiate(this._item.GetITemPrefabs(), dropLocation.position, Quaternion.identity).GetComponent<ItemPickUp>();
        itemPickUp.transform.position += (dropLocation.forward * 2) + new Vector3(0, 2f, 0);
        itemPickUp.SetItemDrop(itemToDrop);
        this.DecreaseStack(stack);
    }
    #endregion

    public void Equip()
    {
        if (this._item != null)
        {
            if (_item as EquipmentItem)
            {
                (_item as EquipmentItem).Equip();
                this.Clear();
            }
            else if (_item as FoodItem)
            {
                (_item as FoodItem).Equip();
            }
            else if (_item as WeaponConfig)
            {
                _item.Equip();
                this._isActive = true;
            }
        }
    }

    public void UnEquip()
    {
        this._isActive = false;
        (_item as WeaponConfig).UnEquip();
    }
}
