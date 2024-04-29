using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BuildingData : ISaveData
{
    string ISaveData.id { get; set; } = System.Guid.NewGuid().ToString();
    public List<BuildItemData> buildItems;

    public BuildingData()
    {
        buildItems = new List<BuildItemData>();
    }

    public void AddItem(BuildItemData item)
    {
        buildItems.Add(item);
    }

    public void RemoveItem(BuildItemData item)
    {
        buildItems.Remove(item);
    }

    public void Clear()
    {
        buildItems.Clear();
    }

    public object CaptureState()
    {
        return null;
    }

    public void RestoreState(object state)
    {

    }
}
[System.Serializable]
public class BuildItemData
{
    public Vector3 position;
    public Quaternion rotation;
    public string prefabName;
    public BuildItemData() { }
    public BuildItemData(Vector3 position, Quaternion rotation, string prefabName)
    {
        this.position = position;
        this.rotation = rotation;
        this.prefabName = prefabName;
    }
}
