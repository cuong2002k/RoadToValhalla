using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using TMPro;
public class SlotUI : SlotView, IPointerClickHandler
{
    private UI_DragDropManager _dragDropManager;

    [SerializeField] private Image _iconActive;
    public InventoryModel Inventory { get; private set; }
    [SerializeField] Color _defaultColor;
    [SerializeField] Color _activeColor;
    public Action<SlotUI, PointerEventData> OnClickAction = delegate { };
    public Action<int, int, InventoryModel, InventoryModel> OnDropAction = delegate { };

    private void Start()
    {
        _dragDropManager = GetComponentInParent<UI_DragDropManager>();
    }


    public void SetInventory(InventoryModel inventory)
    {
        this.Inventory = inventory;
    }



    public ItemStack GetItemUI() => _itemUI;
    public void SetItemUI(ItemStack item)
    {
        this._itemUI = item;
        SetUI();
    }

    public void SetUI()
    {
        if(_itemUI == null) return;
        DisplayUI(!_itemUI.IsEmpty());
        SetUIValue();
    }

    private void DisplayUI(bool active)
    {
        if (_itemIcon == null) Debug.Log("ItemIcon is null");
        if (_stackItemText == null) Debug.Log("ItemStackUI is null");

        if (_itemIcon != null && _stackItemText != null)
        {
            this._itemIcon.gameObject.SetActive(active);
            this._stackItemText.gameObject.SetActive(active);
        }
    }

    public void SetUIValue()
    {
        if (!_itemUI.IsEmpty())
        {
            SetColorActive(_itemUI.GetActive());
            this._itemIcon.sprite = this._itemUI.GetItem().GetItemIcon();
            if (_itemUI.GetStack() <= 1) _stackItemText.gameObject.SetActive(false);
            this._stackItemText.text = "" + this._itemUI.GetStack() + "/" + this._itemUI.GetItem().GetMaxStacks();

        }
        else
        {
            SetColorActive(false);
            this._itemIcon.sprite = null;
            this._stackItemText.text = "0";
        }
    }

    public void SetColorActive(bool active)
    {
        if (active)
        {
            _iconActive.color = _activeColor;
        }
        else
        {
            _iconActive.color = _defaultColor;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (_dragDropManager.slotMove == null)
            {
                OnClickAction?.Invoke(this, eventData);
            }
            else
            {

                int fromSlotID = _dragDropManager.slotMove.GetSlotID();
                int toSlotID = this._slotID;
                InventoryModel fromInventory = _dragDropManager.slotMove.Inventory;
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
