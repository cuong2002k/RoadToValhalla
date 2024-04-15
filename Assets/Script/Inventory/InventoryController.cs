using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class InventoryController : Container, IBind<InventoryData>
{
    [field: SerializeField] public string id { get; set; } = System.Guid.NewGuid().ToString();

    #region Unity Call Back
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
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

    public void Bind(InventoryData data)
    {
        _inventoryModel.Bind(data, this._inventorySize);
        data.id = this.id;
    }
    
    

}


