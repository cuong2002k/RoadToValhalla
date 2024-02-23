using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class InventoryModel
{
    [SerializeField] private ObserverArray<ItemStack> _inventory;

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

    public ItemStack this[int index]
    {
        get => _inventory[index];
        set => _inventory[index] = value;
    }
    public void Swap(int source, int target) => _inventory.Swap(source, target);
    public int AddItem(ItemStack item)
    {
        int stack = item.GetStack();
        for (int i = 0; i < this._inventory.Length; i++)
        {
            if (stack == 0) return 0;
            if (this._inventory[i].IsEmpty()) continue;
            stack = this._inventory[i].AddStack(stack);
        }

        for (int i = 0; i < this._inventory.Length; i++)
        {
            if (stack == 0) return 0;
            if (!this._inventory[i].IsEmpty()) continue;
            this._inventory[i].SetItem(item.GetItem());
            stack = this._inventory[i].AddStack(stack);
        }
        return stack;
    }
    public bool TryRemove(ItemStack item) => _inventory.TryRemove(item);
    public void Invoke() => _inventory.Invoke();
}
