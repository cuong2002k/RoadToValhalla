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
        _slotUIContainer = new List<SlotUI>(size);
    }

    public void RefestInventoryUI(IList<ItemStack> items)
    {
        for (int i = 0; i < _slotUIContainer.Count; i++)
        {
            _slotUIContainer[i].SetItemUI(items[i]);
        }
    }

    public void Initialize(int size, Transform transformSpawn)
    {
        Transform inventoryGroup = transformSpawn;
        for (int index = 0; index < size; index++)
        {
            SlotUI slotUI = Object.Instantiate(slotUIPrefabs, inventoryGroup).GetComponent<SlotUI>();
            slotUI.SetSlotID(index);
            _slotUIContainer.Add(slotUI);
        }
    }

    public SlotUI this[int index]
    {
        get => _slotUIContainer[index];
        set => _slotUIContainer[index] = value;
    }

}

