using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour, ISelectable, IInteractable
{
    [SerializeField] ItemStack _item = new ItemStack();
    private InventoryModel inventory;

    private void Start()
    {
        inventory = InventoryController.Instance.InventoryModel;
    }

    public ItemStack ItemStack
    {
        get => this._item;
        set => this._item = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AddItemToInventory();
        }
    }

    public string GetNameItemSelect()
    {
        return _item.GetItem().GetItemName();
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
            int stackRemaining = inventory.AddItem(_item);
            inventory.Invoke();
            _item.SetStack(stackRemaining);
            if (stackRemaining == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
