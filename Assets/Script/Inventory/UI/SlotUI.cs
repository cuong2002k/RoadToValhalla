using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using TMPro;
public class SlotUI : SlotView, IPointerClickHandler
{
    public InventoryModel Inventory { get; private set; }
    public void SetInventory(InventoryModel inventoryPanel)
    {
        this.Inventory = inventoryPanel;
    }

    public Action<SlotUI, PointerEventData> OnClickAction = delegate { };
    public Action<int, int, InventoryModel, InventoryModel> OnDropAction = delegate { };

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (UIManager.Instance.slotMove == null)
            {
                OnClickAction?.Invoke(this, eventData);
            }
            else
            {

                int fromSlotID = UIManager.Instance.slotMove.GetSlotID();
                int toSlotID = this._slotID;
                InventoryModel fromInventory = UIManager.Instance.slotMove.Inventory;
                InventoryModel toInventory = this.Inventory;
                OnDropAction?.Invoke(fromSlotID, toSlotID, fromInventory, toInventory);
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ItemStack equipItem = this.Inventory[this._slotID];
            equipItem.Equip();
            Inventory.Invoke();
        }
    }


}
