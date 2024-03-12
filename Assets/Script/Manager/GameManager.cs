using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    [HideInInspector] public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }
    #endregion

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        DontDestroyOnLoad(this.gameObject);
    }

}
