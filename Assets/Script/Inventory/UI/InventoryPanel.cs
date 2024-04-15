using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private List<SlotUI> _slotUIContainer;
    [SerializeField] private GameObject slotUIPrefabs;
    private void Awake()
    {
        slotUIPrefabs = Resources.Load<GameObject>("InventorySlotUI");
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
        if (_slotUIContainer == null)
        {
            _slotUIContainer = new List<SlotUI>(size);
        }

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

    public void Clear()
    {
        foreach (SlotUI slotUI in _slotUIContainer)
        {
            Destroy(slotUI.gameObject);
        }
    }

}

