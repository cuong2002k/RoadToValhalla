using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class InventoryController : Container, ISaveData
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

    public object CaptureState()
    {
        return _inventoryModel.GetData();
    }

    public void RestoreState(object state)
    {
        KeyValuePair<string, int>[] dataRestore = (KeyValuePair<string, int>[])state;
        _inventoryModel.RestoreData(dataRestore);
        RefestUI();
    }
}


