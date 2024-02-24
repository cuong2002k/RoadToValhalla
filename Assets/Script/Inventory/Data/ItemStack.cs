using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStack
{
    [SerializeField] private BaseItem _item = null;
    [SerializeField] private int _stack = 0;

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
    }

    #endregion

    #region Get & Set
    public BaseItem GetItem() => this._item;
    public int GetStack() => this._stack;

    public void SetItemStack(ItemStack item)
    {
        this._item = item.GetItem();
        this._stack = item.GetStack();
    }

    public void SetStack(int stack)
    {
        this._stack = stack;
        if (this._stack == 0) this.Clear();
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
        return (item != null) && (item.GetItem().Equals(this._item));
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
    }

    public void DropItem(ItemStack itemToDrop)
    {
        Transform transformSpawn = GameManager.Instance.Player.transform;
        ItemPickUp itemPickUp = Object.Instantiate(this._item.GetITemPrefabs()).GetComponent<ItemPickUp>();
        itemPickUp.transform.position = transformSpawn.forward + new Vector3(0, 2f, 0);
        itemPickUp.ItemStack.SetItemStack(itemToDrop);
        this.DecreaseStack(itemToDrop.GetStack());
    }

}
