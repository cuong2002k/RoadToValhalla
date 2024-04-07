using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    [HideInInspector] public static GameManager Instance;
    public bool test = false;
    public float timeScale = 0.1f;
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

    private void Update()
    {
        if (test)
        {
            test = false;
            Time.timeScale = timeScale;
        }
    }

}
