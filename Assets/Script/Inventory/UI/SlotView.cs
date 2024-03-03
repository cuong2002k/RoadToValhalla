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
    public bool IsEmpty() => this._itemUI.IsEmpty();
}
