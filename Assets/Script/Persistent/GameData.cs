using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData : ISaveData
{
    [field: SerializeField] public string id { get; set; } = System.Guid.NewGuid().ToString();
    public string SaveName;
    public string CurrentLevel;
    public PlayerGameData PlayerGameData;
    public InventoryData InventoryData;
    public InventoryData HotbarData;
    public EquipmentData EquipmentData;


}
