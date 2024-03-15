using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData : ISaveData
{
    [field: SerializeField] public string id { get; set; } = System.Guid.NewGuid().ToString();
    public ItemStack[] items;
    public int size;
}
