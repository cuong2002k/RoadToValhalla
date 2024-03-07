using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public interface ISaveData
{
    System.Guid id { get; set; }
}

public interface IBind<TData> where TData : ISaveData
{
    System.Guid id { get; set; }
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
        Bind<PlayerManager, PlayerGameData>(gameData.playerGameData);
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

    public void NewGame()
    {
        gameData = new GameData
        {
            SaveName = "NewCharacter",
            CurrentLevel = "Level0"
        };
        SceneManager.LoadScene(gameData.CurrentLevel);
    }

    public void SaveGame()
    {
        dataService.SaveGame(gameData);
    }

    public void LoadGame(string SaveName)
    {
        gameData = dataService.LoadGame(SaveName);
        if (string.IsNullOrWhiteSpace(gameData.CurrentLevel))
        {
            SceneManager.LoadScene(gameData.CurrentLevel);
        }
        SceneManager.LoadScene(gameData.CurrentLevel);
    }

    public void DeleteGame(string SaveName)
    {
        dataService.DeleteGame(SaveName);
    }
}
