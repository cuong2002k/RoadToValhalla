using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataService
{
    public void SaveGame(Dictionary<string, object> gameData, bool overwrite = true);
    public Dictionary<string, object> LoadGame(string SaveName);
    public void DeleteGame(string SaveName);
}
