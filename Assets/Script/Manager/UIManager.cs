using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerInventoryPanel;
    [SerializeField] private GameObject _craftingPanel;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        InputManager.OpenInventoryEvent += HandlerUIInput;
    }

    private void OnDisable()
    {
        InputManager.OpenInventoryEvent -= HandlerUIInput;
    }

    public void HandlerUIInput()
    {
        if (_playerInventoryPanel.activeSelf)
        {
            _playerInventoryPanel.SetActive(false);
            _craftingPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            _playerInventoryPanel.SetActive(true);
            _craftingPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
