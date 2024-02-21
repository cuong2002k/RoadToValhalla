using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class InventoryPanel
{
    [SerializeField] private List<SlotUI> _slotUIContainer;
    private GameObject slotUIPrefabs;
    public InventoryPanel(int size)
    {
        slotUIPrefabs = Resources.Load<GameObject>("InventorySlotUI");

        if (slotUIPrefabs != null) Debug.Log("Slot prefabs is null");
        if (InventoryController.Instance.InventoryPrefabs != null) Debug.Log("inventory prefabs is null");
        _slotUIContainer = new List<SlotUI>(size);
        Transform inventoryGroup = InventoryController.Instance.InventoryPrefabs.transform;
        for (int index = 0; index < size; index++)
        {
            SlotUI slotUI = Object.Instantiate(slotUIPrefabs, inventoryGroup).GetComponent<SlotUI>();
            slotUI.SetSlotID(index);
            _slotUIContainer.Add(slotUI);
        }
    }

    public void RefestInventoryUI(IList<ItemStack> items)
    {
        for (int i = 0; i < _slotUIContainer.Count; i++)
        {
            _slotUIContainer[i].SetItemUI(items[i]);
        }
    }
}

