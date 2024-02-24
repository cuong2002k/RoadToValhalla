using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    [HideInInspector] public static GameManager Instance;
    public GameObject Player;
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
    }
    #endregion

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        DontDestroyOnLoad(this.gameObject);
    }

}
