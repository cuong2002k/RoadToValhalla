using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour, ISelectable, IInteractable
{
    [SerializeField] ItemStack _item = new ItemStack();
    [SerializeField] private InventoryController _inventory;

    private void Start()
    {
        _inventory = PlayerUIManager.Instance.InventoryController;
    }

    public ItemStack ItemStack
    {
        get => this._item;
        set => this._item = value;
    }

    public string GetNameItemSelect()
    {
        if (_item != null && !_item.IsEmpty())
            return _item.GetItem().GetItemName();
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
        if (!_item.IsEmpty())
        {
            int stackRemaining = _inventory.AddItem(_item);
            _inventory.RefestUI();
            _item.SetStack(stackRemaining);
            if (stackRemaining == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
