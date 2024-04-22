using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CraftContent : MonoBehaviour
{
    [SerializeField] int id;

    [SerializeField] private TextMeshProUGUI _nameCraftItem;
    [SerializeField] private Image _imageCraftItem;

    [SerializeField] private ItemCraft _item;

    [SerializeField] private CraftItemManager _craftItemManager;
    private void Start()
    {
        _craftItemManager = GetComponentInParent<CraftItemManager>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    public void SetItem(ItemCraft item)
    {
        if (item != null)
        {
            this._item = item;
            _imageCraftItem.sprite = item.Item.GetItemIcon();
            _nameCraftItem.text = item.Item.GetItemName();
        }

    }

    public void OnClick()
    {
        _craftItemManager.CraftItemDetails.SetItemCraft(_item);
    }


}
