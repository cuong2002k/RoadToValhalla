using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBarContainer : Container
{
    private InputManager inputManager;
    public int Hotbar = 0;
    protected override void Start()
    {
        base.Start();
        inputManager = InputManager.Instance;
    }

    private void Update()
    {
        Hotbar = inputManager.HotBarInput;
        if (Hotbar > 0)
        {
            ItemStack itemInSlot = this._inventoryModel[Hotbar - 1];
            if (!itemInSlot.IsEmpty())
            {
                itemInSlot.Equip();
                _inventoryModel.Invoke();
            }
        }
    }
}
