using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public void ContinueGame()
    {
        PauseMenu.SetActive(false);
        GameManager.Instance.ActiveControl(false);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void SaveGame()
    {
        SaveLoadSystem.Instance.SaveGame();
    }

    public void BackToMenuMain()
    {
        LoadingSceneManager.Instance.LoadingScene("MainMenu");
    }
}
