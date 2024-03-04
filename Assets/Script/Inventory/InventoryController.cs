using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class InventoryController : Container
{

    #region Unity Call Back
    protected override void Start()
    {
        base.Start();
        _inventoryModel.Invoke();
    }

    #endregion 

    public int AddItem(ItemStack itemToAdd)
    {
        int stackRemaining = _inventoryModel.AddItem(itemToAdd);
        RefestUI();
        return stackRemaining;
    }

    public void RefestUI()
    {
        this._inventoryModel.Invoke();
    }

}
