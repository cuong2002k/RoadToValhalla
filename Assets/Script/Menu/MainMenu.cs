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
        LoadingSceneManager.Instance.LoadingScene("Level0", slider);
    }

    public void LoadGame()
    {
        menu.SetActive(false);
        load.SetActive(true);
        LoadingSceneManager.Instance.LoadingScene("Level0", slider);
        SaveLoadSystem.Instance.LoadGameState();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
