using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(SaveLoadSystem))]
public class SaveLoadManagerEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        SaveLoadSystem saveLoadSystem = (SaveLoadSystem)target;
        string nameSave = (target as SaveLoadSystem).GameData.SaveName;
        DrawDefaultInspector();

        if (GUILayout.Button("New Game"))
        {
            saveLoadSystem.NewGame();
        }

        if (GUILayout.Button("Save Game"))
        {
            saveLoadSystem.SaveGame();
        }

        if (GUILayout.Button("Load Game"))
        {
            saveLoadSystem.LoadGame(nameSave);
        }

        if (GUILayout.Button("Delete Game"))
        {
            saveLoadSystem.DeleteGame(nameSave);
        }

    }
}
