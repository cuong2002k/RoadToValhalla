using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    #region Singleton
    public static PlayerUIManager Instance;
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

        PlayerHubManager = GetComponentInChildren<PlayerHudManger>();
        InventoryController = GetComponentInChildren<InventoryController>();
        HotBarContainer = GetComponentInChildren<PlayerHotBarContainer>();
        DragDropManager = GetComponent<UI_DragDropManager>();
    }
    #endregion

    #region Component
    // [HideInInspector]
    [HideInInspector] public PlayerHudManger PlayerHubManager;
    [HideInInspector] public InventoryController InventoryController;
    [HideInInspector] public PlayerHotBarContainer HotBarContainer;
    [HideInInspector] public UI_DragDropManager DragDropManager;
    #endregion

    [SerializeField] private GameObject _playerInventoryPanel;
    [SerializeField] private GameObject _craftingPanel;
    [SerializeField] private GameObject _mousePoint;


    private void OnEnable()
    {
        InputManager.OpenInventoryEvent += HandlerUIInput;
    }

    private void OnDisable()
    {
        InputManager.OpenInventoryEvent -= HandlerUIInput;
    }

    private void Update()
    {
        _mousePoint.gameObject.transform.position = Input.mousePosition;
    }

    public void HandlerUIInput()
    {
        if (_playerInventoryPanel.activeSelf)
        {
            _playerInventoryPanel.SetActive(false);
            _craftingPanel.SetActive(false);
            DragDropManager.RestartMouseSlot();
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
