using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSlotUI : SlotView
{
    public void SetItemUI(BaseItem item, int stack)
    {
        this._itemIcon.sprite = item.GetItemIcon();
        this._stackItemText.text = stack.ToString();
        SetActive(true);
    }

    public void SetActive(bool active)
    {
        _stackItemText.gameObject.SetActive(active);
        this._itemIcon.gameObject.SetActive(active);
    }

    public void Clear()
    {
        this._itemIcon.sprite = null;
        this._stackItemText.text = "";
        SetActive(false);
    }
}
