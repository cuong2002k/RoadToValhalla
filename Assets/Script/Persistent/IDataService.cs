using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataService
{
    public void SaveGame(GameData gameData, bool overwrite = true);
    public GameData LoadGame(string SaveName);
    public void DeleteGame(string SaveName);
}
