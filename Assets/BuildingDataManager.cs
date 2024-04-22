using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDataManager : MonoBehaviour, IBind<BuildingData>
{
    public string id { get; set; } = System.Guid.NewGuid().ToString();
    public BuildingData buildingData = new BuildingData();
    public List<GameObject> BuildgameObjects = new List<GameObject>();
    Dictionary<string, GameObject> buildingObjectsDictionary = new Dictionary<string, GameObject>();

    private void Awake()
    {
        foreach (GameObject gameObject in BuildgameObjects)
        {
            buildingObjectsDictionary.Add(gameObject.name, gameObject);
        }
    }

    public void Bind(BuildingData data)
    {
        buildingData.buildItems = data.buildItems;
        foreach (BuildItemData item in buildingData.buildItems)
        {
            GameObject gameObject = Instantiate(buildingObjectsDictionary[item.prefabName], item.position, item.rotation);
        }

    }

    public void AddItemBuilding(BuildItemData item)
    {
        buildingData.AddItem(item);
        SaveLoadSystem.Instance.GameData.BuildingData = buildingData;
    }

    public void RemoveItemBuild(BuildItemData item)
    {
        buildingData.RemoveItem(item);
        SaveLoadSystem.Instance.GameData.BuildingData = buildingData;
    }
}
