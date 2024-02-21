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
    }

    public ItemStack GetItem(int index) => _inventory.Get(index);
    public void Swap(int source, int target) => _inventory.Swap(source, target);
    public bool AddItem(ItemStack item) => _inventory.TryAdd(item);
    public bool TryRemove(ItemStack item) => _inventory.TryRemove(item);
    public void Invoke() => _inventory.Invoke();
}
