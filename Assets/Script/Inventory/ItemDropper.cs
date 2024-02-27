using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    UIManager _uIManager;
    private void Start()
    {
        _uIManager = UIManager.Instance;
    }
    public void DropItemSpawner(ItemStack item, int stack)
    {
        if (stack <= item.GetStack())
        {
            DropItem(item, stack);
        }
    }

    public void DropItem(ItemStack item, int stack)
    {
        item.DropItem(item, GetDropLocation(), stack);
        this._uIManager.slotMove.Inventory.Invoke();
        _uIManager.RestartMouseSlot();
    }

    public Transform GetDropLocation()
    {
        return this.transform;
    }
}
