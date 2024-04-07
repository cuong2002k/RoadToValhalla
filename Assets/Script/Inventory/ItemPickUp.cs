using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour, ISelectable, IInteractable
{
    [SerializeField] private BaseItem _item = null;
    [SerializeField] private int _stack = 1;
    [SerializeField] private InventoryController _inventory;

    private void Start()
    {
        _inventory = PlayerUIManager.Instance.InventoryController;
    }

    public void SetItemDrop(ItemStack itemStack)
    {
        _item = itemStack.GetItem();
        _stack = itemStack.GetStack();
    }


    public string GetNameItemSelect()
    {
        if (_item != null)
            return _item.GetItemName();
        return "";
    }

    public void Interact()
    {
        if (_item != null)
        {
            AddItemToInventory();
        }

    }

    public void AddItemToInventory()
    {
        if (_item != null)
        {
            int stackRemaining = _inventory.AddItem(new ItemStack(_item, _stack));
            _stack = stackRemaining;
            _inventory.RefestUI();
            if (stackRemaining == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
