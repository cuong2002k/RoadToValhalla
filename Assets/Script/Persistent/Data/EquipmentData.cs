using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentData : ISaveData
{
    public string id { get; set; } = System.Guid.NewGuid().ToString();
    public string[] EquipData;

    public object CaptureState()
    {
        throw new System.NotImplementedException();
    }

    public void RestoreState(object state)
    {
        throw new System.NotImplementedException();
    }
}