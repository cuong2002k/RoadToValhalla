using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataService : IDataService
{
    private string pathName = "";
    private string extention = "";
    private ISerializer serializer;

    public FileDataService(ISerializer serializer)
    {
        this.pathName = Application.persistentDataPath;
        this.extention = ".json";
        this.serializer = serializer;
    }

    private string GetPathLocation(string SaveName)
    {
        return Path.Combine(pathName, SaveName + extention);
    }

    public void SaveGame(GameData gameData, bool overwrite = true)
    {
        string location = GetPathLocation(gameData.SaveName);
        if (!overwrite && File.Exists(location))
        {
            throw new Exception($"The File '{location}' already exits cannot overwriten");
        }
        File.WriteAllText(location, serializer.Serialize<GameData>(gameData));
        Debug.Log($"The file has been successfully saved at the path {location}");

    }


    public GameData LoadGame(string SaveName)
    {
        string location = GetPathLocation(SaveName);
        if (!File.Exists(location))
        {
            throw new Exception($"No permisted gamedata with file name in '{location}'");
        }

        return serializer.DeSerialize<GameData>(File.ReadAllText(location));
    }

    public void DeleteGame(string SaveName)
    {
        string location = GetPathLocation(SaveName);
        if (!File.Exists(SaveName))
        {
            throw new Exception($"No permisted gamedata with file name in '{location}'");
        }
        File.Delete(location);
    }
}
