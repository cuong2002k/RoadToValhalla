using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class InventoryModel
{
    [SerializeField] private ObserverArray<ItemStack> _inventory;


    //event refest inventory UI
    public event Action<ItemStack[]> OnInventoryChange
    {
        add => _inventory.OnArrayChange += value;
        remove => _inventory.OnArrayChange -= value;
    }

    public InventoryModel(int size)
    {
        _inventory = new ObserverArray<ItemStack>(size);
        for (int i = 0; i < size; i++)
        {
            _inventory[i] = new ItemStack();
        }
    }

    public KeyValuePair<string, int>[] GetData()
    {
        KeyValuePair<string, int>[] inventoryData = new KeyValuePair<string, int>[_inventory.Length];
        for (int i = 0; i < _inventory.Length; i++)
        {
            if (_inventory[i] != null && !_inventory[i].IsEmpty())
            {
                inventoryData[i] = new KeyValuePair<string, int>(_inventory[i].GetItem().ID, _inventory[i].GetStack());
            }
        }
        return inventoryData;
    }

    public void RestoreData(KeyValuePair<string, int>[] dataRestore)
    {
        for (int i = 0; i < dataRestore.Length; i++)
        {
            if (dataRestore[i].Key != null)
            {
                BaseItem baseItem = ItemDatabase.GetItemWithID(dataRestore[i].Key);
                int stack = dataRestore[i].Value;
                _inventory[i].SetItemStack(baseItem, stack);
            }
        }
    }

    public int CountItemStack(BaseItem item)
    {
        if (item == null) return 0;
        int Count = 0;
        for (int i = 0; i < _inventory.Length; i++)
        {
            if (_inventory[i].IsEmpty()) continue;
            if (item.ID == _inventory[i].GetItem().ID)
            {
                Count += _inventory[i].GetStack();
            }
        }
        return Count;
    }

    public ItemStack this[int index]
    {
        get => _inventory[index];
        set => _inventory[index] = value;
    }
    public void Swap(int source, int target) => _inventory.Swap(source, target);
    public int AddItem(ItemStack item)
    {

        int stack = item.GetStack();
        // find contain item slot => combine stack
        for (int i = 0; i < this._inventory.Length; i++)
        {
            if (stack == 0)
            {
                return 0;
            }
            if (this._inventory[i].IsEmpty()) continue;
            if (_inventory[i].IsItemEqual(item))
            {
                stack = this._inventory[i].AddStack(stack);
            }

        }

        // // find free slot => add to inventory
        int indexFreeSlot = FindFirstFreeSlot();

        if (indexFreeSlot != -1)
        {
            while (stack > 0)
            {
                this._inventory[indexFreeSlot].SetItem(item.GetItem());
                stack = this._inventory[indexFreeSlot].AddStack(stack);
            }
            if (stack == 0)
            {
                return stack;
            }
        }

        Debug.Log("Full slot inventory");
        return stack;
    }

    public int MinusItem(BaseItem item, int stackToMinus)
    {
        int stack = stackToMinus;
        // find contain item slot => combine stack
        for (int i = 0; i < this._inventory.Length; i++)
        {
            if (stack == 0) return 0;
            if (this._inventory[i].IsEmpty()) continue;
            if (_inventory[i].Id == item.ID)
            {
                if (_inventory[i].GetStack() >= stack)
                {
                    this._inventory[i].DecreaseStack(stack);
                    stack = 0;
                }
                else
                {
                    stack -= _inventory[i].GetStack();
                    this._inventory[i].Clear();
                }
            }

        }
        Invoke();
        return stack;
    }

    public int FindFirstFreeSlot()
    {
        for (int i = 0; i < _inventory.Length; i++)
        {
            if (_inventory[i].IsEmpty()) return i;
        }
        return -1;
    }

    //call event
    public void Invoke() => _inventory.Invoke();
}
