using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using TMPro;
public class SlotUI : SlotView, IPointerClickHandler
{
    public InventoryModel Inventory { get; set; }
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
                OnDropAction?.Invoke(UIManager.Instance.slotMove.GetSlotID(), this._slotID, UIManager.Instance.slotMove.Inventory, this.Inventory);
            }

        }
    }


}
