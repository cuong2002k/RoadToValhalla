using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentData : ISaveData
{
    public string id { get; set; } = System.Guid.NewGuid().ToString();
    public EquipmentItem[] EquipData;

}