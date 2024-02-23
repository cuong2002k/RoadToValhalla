using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SlotView : MonoBehaviour
{
    [SerializeField] protected int _slotID;
    [SerializeField] protected ItemStack _itemUI;

    [SerializeField] protected Image _itemIcon;
    [SerializeField] protected TextMeshProUGUI _stackItemText;

    public int GetSlotID() => this._slotID;
    public void SetSlotID(int id) => this._slotID = id;
    public Image GetItemIcon() => this._itemIcon;
    public TextMeshProUGUI GetStackText() => this._stackItemText;

    public void SetItemUI(ItemStack item)
    {
        this._itemUI = item;
        SetUI();
    }
    public ItemStack GetItemUI() => _itemUI;

    public void SetUI()
    {
        DisplayUI(_itemUI != null);
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
            this._itemIcon.sprite = this._itemUI.GetItem().GetItemIcon();
            this._stackItemText.text = "" + this._itemUI.GetStack() + "/" + this._itemUI.GetItem().GetMaxStacks();
        }
        else
        {
            this._itemIcon.sprite = null;
            this._stackItemText.text = "0";
        }
    }

    public bool IsEmpty() => this._itemUI.IsEmpty();
}
