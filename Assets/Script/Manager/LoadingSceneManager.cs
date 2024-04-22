using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingSceneManager : PersistentSingleton<LoadingSceneManager>
{

    public void LoadingScene(string sceneName, Slider slider = null)
    {
        StartCoroutine(LoadScene(sceneName, slider));
    }

    private IEnumerator LoadScene(string sceneName, Slider slider = null)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            float duration = Mathf.Clamp01(operation.progress / 0.9f);
            if (slider != null)
            {
                slider.value = duration;
            }
            yield return new WaitForSeconds(duration);
        }
    }
}
