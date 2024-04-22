using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : PersistentSingleton<GameManager>
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ActiveControl(bool active)
    {
        Camera.main.transform.root.GetComponent<PlayerController>().enabled = !active;
        Camera.main.transform.GetComponentInParent<CameraController>().enabled = !active;
        Cursor.visible = active;
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
    }


}
