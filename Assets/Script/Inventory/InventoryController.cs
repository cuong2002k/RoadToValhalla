using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class InventoryController : Container
{
    public static InventoryController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public BaseItem item;
    public BaseItem items1;

    protected override void Start()
    {
        base.Start();
        _inventoryModel.AddItem(new ItemStack(item, 20));
        _inventoryModel.AddItem(new ItemStack(items1, 20));
        _inventoryModel.Invoke();
    }

    public void AddITem(ItemStack itemToAdd)
    {
        _inventoryModel.AddItem(itemToAdd);
        _inventoryModel.Invoke();
    }

}
