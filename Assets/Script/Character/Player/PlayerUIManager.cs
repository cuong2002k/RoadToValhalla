using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    #region Singleton
    public static PlayerUIManager Instance;
    protected void Awake()
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
        PlayerPopUpManager = GetComponentInChildren<PlayerPopUpManager>();
    }
    #endregion
    #region Component
    // [HideInInspector]
    [HideInInspector] public PlayerHudManger PlayerHubManager;
    [HideInInspector] public InventoryController InventoryController;
    [HideInInspector] public PlayerHotBarContainer HotBarContainer;
    [HideInInspector] public UI_DragDropManager DragDropManager;
    [HideInInspector] public PlayerPopUpManager PlayerPopUpManager;
    #endregion

    [SerializeField] private GameObject _playerInventoryPanel;
    [SerializeField] private GameObject _craftingPanel;
    [SerializeField] private GameObject _mousePoint;
    [SerializeField] private GameObject _PauseMenu;

    private void Start()
    {
        _playerInventoryPanel.SetActive(false);
        _craftingPanel.SetActive(false);
        DragDropManager.RestartMouseSlot();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _mousePoint.gameObject.transform.position = Input.mousePosition;
        if (PlayerManager.Instance.IsDead)
        {
            _playerInventoryPanel.SetActive(false);
            _craftingPanel.SetActive(false);
            DragDropManager.RestartMouseSlot();
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            HandlerUIInput(!_playerInventoryPanel.activeInHierarchy);
            ChestController.Instance.OpenChest(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenPauseGame(!_PauseMenu.activeInHierarchy);
        }
    }

    public void HandlerUIInput(bool active)
    {
        if (PlayerManager.Instance.IsDead) return;
        _playerInventoryPanel.SetActive(active);
        _craftingPanel.SetActive(active);
        DragDropManager.RestartMouseSlot();
        GameManager.Instance.ActiveControl(active);
    }

    public void OpenPauseGame(bool active)
    {
        _playerInventoryPanel.SetActive(false);
        _craftingPanel.SetActive(false);
        _PauseMenu.SetActive(active);
        DragDropManager.RestartMouseSlot();
        GameManager.Instance.ActiveControl(active);
    }






}
