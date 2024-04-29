using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData : ISaveData
{
    [field: SerializeField] public string id { get; set; } = System.Guid.NewGuid().ToString();
    public ItemStack[] items;
    public int size;

    public object CaptureState()
    {
        throw new System.NotImplementedException();
    }

    public void RestoreState(object state)
    {
        throw new System.NotImplementedException();
    }
}
