using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Craft Item", menuName = "Data/ItemSO/Craft Item")]
public class CraftItem : BaseItem
{
    private void Start()
    {
        this._itemType = ItemType.CraftItem;
    }

}
