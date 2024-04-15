using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CraftItemDetails : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public ItemCraft CurrentItemCraft;
    public List<IngredientSlotUI> ingredientSlotUIs = new List<IngredientSlotUI>();
    [SerializeField] private Button craftButton;

    private void Start()
    {
        craftButton.onClick.AddListener(CraftItem);
    }

    public void SetItemCraft(ItemCraft itemCraft)
    {
        if (itemCraft != null)
        {
            CurrentItemCraft = itemCraft;
            itemImage.sprite = CurrentItemCraft.Item.GetItemIcon();
            itemName.text = CurrentItemCraft.Item.GetItemName();
            itemDescription.text = CurrentItemCraft.Item.GetDescription();
            ClearingredientSlotUIs();
            for (int i = 0; i < CurrentItemCraft.ingredients.Count; i++)
            {
                ingredientSlotUIs[i].SetItemUI(CurrentItemCraft.ingredients[i].Item, CurrentItemCraft.ingredients[i].Count);
            }
        }
    }

    public void ClearingredientSlotUIs()
    {
        for (int i = 0; i < ingredientSlotUIs.Count; i++)
        {
            ingredientSlotUIs[i].Clear();
        }
    }

    public void CraftItem()
    {
        if (CanCraft())
        {
            InventoryController inventoryController = PlayerUIManager.Instance.InventoryController;
            int stack = inventoryController.AddItem(new ItemStack(CurrentItemCraft.Item, CurrentItemCraft.Count));
            if (stack > 0)
            {
                Debug.Log("Not contain slot");
            }
            else
            {
                for (int i = 0; i < CurrentItemCraft.ingredients.Count; i++)
                {
                    BaseItem item = CurrentItemCraft.ingredients[i].Item;
                    int needStack = CurrentItemCraft.ingredients[i].Count;
                    inventoryController.InventoryModel.MinusItem(item, needStack);
                }
                inventoryController.Invoke();
            }
        }
        else
        {
            Debug.Log("Not Enough ingredient");
        }
    }

    private bool CanCraft()
    {
        for (int i = 0; i < CurrentItemCraft.ingredients.Count; i++)
        {
            BaseItem item = CurrentItemCraft.ingredients[i].Item;
            int needStack = CurrentItemCraft.ingredients[i].Count;
            if (PlayerUIManager.Instance.InventoryController.InventoryModel.CountItemStack(item) < needStack)
            {
                return false;
            }
        }
        return true;
    }

}
