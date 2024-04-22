using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, ISelectable, IInteractable
{
    public InventoryModel _inventoryModel;
    private void Start()
    {
        _inventoryModel = new InventoryModel(12);
    }
    public string GetNameItemSelect()
    {
        return "Press E Open";
    }

    public void Interact()
    {
        ChestController.Instance.OpenChest(true);
        PlayerUIManager.Instance.HandlerUIInput(true);
        ChestController.Instance.SetChestDisplay(12, _inventoryModel);
    }





}
