using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItemManager : MonoBehaviour
{
    [SerializeField] private List<ItemCraft> _itemCrafting = new List<ItemCraft>();
    [SerializeField] private GameObject _craftContentPanel;
    [SerializeField] private GameObject _craftItemPrefabs;
    public CraftItemDetails CraftItemDetails { get; private set; }

    private void Start()
    {
        CraftItemDetails = GetComponentInChildren<CraftItemDetails>();
        foreach (ItemCraft item in _itemCrafting)
        {
            CraftContent craftContent = Instantiate(_craftItemPrefabs, _craftContentPanel.transform).GetComponent<CraftContent>();
            craftContent.SetItem(item);
        }
        CraftItemDetails.SetItemCraft(_itemCrafting[0]);

    }


}


[System.Serializable]
public class ItemCraft
{
    public BaseItem Item;
    public int Count;
    public List<Ingredient> ingredients = new List<Ingredient>();
}
[System.Serializable]
public class Ingredient
{
    public BaseItem Item;
    public int Count;
}