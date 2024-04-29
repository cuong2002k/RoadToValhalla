using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.VisualScripting;

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
    private ISerializer serializer;
    Dictionary<string, object> gameState = new Dictionary<string, object>();

    protected override void Awake()
    {
        base.Awake();
        serializer = new BinarySerializer();
        dataService = new FileDataService(serializer);
    }

    private void OnEnable() => SceneManager.sceneLoaded += OnScenesLoaded;

    private void Ondisable() => SceneManager.sceneLoaded -= OnScenesLoaded;

    private void OnScenesLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu") return;
        foreach (EntitySaving entitySaving in FindObjectsOfType<EntitySaving>())
        {
            if (gameState.ContainsKey(entitySaving.GetUniqueID()))
            {
                entitySaving.RestoreState(gameState[entitySaving.GetUniqueID()]);
            }
        }
    }


    public void SaveGameState()
    {
        foreach (EntitySaving entitySaving in FindObjectsOfType<EntitySaving>())
        {
            gameState[entitySaving.GetUniqueID()] = entitySaving.CaptureState();
        }
        gameState["SaveName"] = "NewCharacter";
        dataService.SaveGame(gameState);
    }

    public void LoadGameState()
    {
        gameState = dataService.LoadGame("NewCharacter");
        foreach (EntitySaving entitySaving in FindObjectsOfType<EntitySaving>())
        {
            if (gameState.ContainsKey(entitySaving.GetUniqueID()))
            {
                entitySaving.RestoreState(gameState[entitySaving.GetUniqueID()]);
            }
        }
    }

}
