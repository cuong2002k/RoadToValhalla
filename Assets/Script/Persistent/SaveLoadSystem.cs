using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public interface ISaveData
{
    [SerializeField] public string id { get; set; }
}

public interface IBind<TData> where TData : ISaveData
{
    public string id { get; set; }
    public void Bind(TData data);
}


public class SaveLoadSystem : PersistentSingleton<SaveLoadSystem>
{
    [SerializeField] private GameData gameData;
    public GameData GameData => gameData;
    private IDataService dataService;
    private void OnEnable() => SceneManager.sceneLoaded += OnScenesLoaded;

    private void Ondisable() => SceneManager.sceneLoaded -= OnScenesLoaded;

    private void OnScenesLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu") return;
        Bind<PlayerManager, PlayerGameData>(gameData.PlayerGameData);
        Bind<InventoryController, InventoryData>(gameData.InventoryData);
        Bind<PlayerHotBarContainer, InventoryData>(gameData.HotbarData);
        Bind<EquipmentManager, EquipmentData>(gameData.EquipmentData);
        Bind<BuildingDataManager, BuildingData>(gameData.BuildingData);
    }

    protected override void Awake()
    {
        base.Awake();
        dataService = new FileDataService(new JsonSerializer());
    }



    public void Bind<T, TData>(TData data) where T : MonoBehaviour, IBind<TData> where TData : ISaveData, new()
    {
        var entity = FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();
        if (entity != null)
        {
            if (data == null)
            {
                data = new TData
                {
                    id = entity.id
                };
            }
            entity.Bind(data);
        }
    }

    public GameData NewGame()
    {
        gameData = new GameData();
        return gameData;
    }

    public void SaveGame()
    {
        dataService.SaveGame(gameData);
    }

    public GameData LoadGame(string SaveName)
    {
        gameData = dataService.LoadGame(SaveName);
        return gameData;
    }

    public void DeleteGame(string SaveName)
    {
        dataService.DeleteGame(SaveName);
    }
}
