using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Slider slider;
    public GameObject menu;
    public GameObject load;

    public void NewGame()
    {
        load.SetActive(true);
        menu.SetActive(false);
        GameData gameData = SaveLoadSystem.Instance.NewGame();
        LoadingSceneManager.Instance.LoadingScene(gameData.CurrentLevel, slider);
    }

    public void LoadGame()
    {
        menu.SetActive(false);
        load.SetActive(true);
        GameData gameData = SaveLoadSystem.Instance.LoadGame("NewCharacter");
        LoadingSceneManager.Instance.LoadingScene(gameData.CurrentLevel, slider);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
