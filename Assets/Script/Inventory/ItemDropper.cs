using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    private UI_DragDropManager _dragDropManager;
    private void Start()
    {
        _dragDropManager = PlayerUIManager.Instance.DragDropManager;
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
        this._dragDropManager.slotMove.Inventory.Invoke();
        _dragDropManager.RestartMouseSlot();
    }

    public Transform GetDropLocation()
    {
        return this.gameObject.transform;
    }
}
