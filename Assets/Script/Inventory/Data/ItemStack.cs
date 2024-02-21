using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStack
{
    [SerializeField] private int _slotID = -1;
    [SerializeField] private BaseItem _item = null;
    [SerializeField] private int _stack = 0;

    #region Constructer
    public ItemStack()
    {
        this._slotID = -1;
        this._item = null;
        this._stack = 0;
    }

    public ItemStack(int slotID)
    {
        this._slotID = slotID;
        this._item = null;
        this._stack = 0;
    }

    public ItemStack(BaseItem item, int stack)
    {
        this._item = item;
        this._stack = stack;
    }

    public ItemStack(BaseItem item, int stack, int slotID)
    {
        this._slotID = slotID;
        this._item = item;
        this._stack = stack;
    }
    #endregion

    #region Get & Set
    public int GetSlotID() { return this._slotID; }
    public BaseItem GetItem() { return this._item; }
    public int GetStack() { return this._stack; }

    public void SetItemStack(ItemStack item)
    {
        this._item = item.GetItem();
        this._stack = item.GetStack();
    }

    public void SetStack(int stack)
    {
        this._stack = stack;
    }

    public void SetItem(BaseItem item)
    {
        this._item = item;
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
        return (item != null) && (item.GetItem() == this._item);
    }

    public bool CanAddTo()
    {
        return this._stack < this._item.GetMaxStacks();
    }

    #endregion


    public int AddStack(int stackToAdd)
    {
        if (this.GetStackAvailable() > stackToAdd)
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

    public ItemStack Copy()
    {
        return new ItemStack(this._item, this._stack, this._slotID);
    }

    public ItemStack SplitStack(int stackToSplit)
    {

        int stack = Mathf.Min(this._stack, stackToSplit);
        ItemStack itemToSplit = this.Copy();
        itemToSplit.SetStack(stack);
        this.DecreaseStack(stack);
        return itemToSplit;
    }

    public void Clear()
    {
        this._item = null;
        this._stack = 0;
    }

}
