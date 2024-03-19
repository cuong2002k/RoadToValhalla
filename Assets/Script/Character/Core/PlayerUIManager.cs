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

    private void Start()
    {
        _playerInventoryPanel.SetActive(false);
        _craftingPanel.SetActive(false);
        DragDropManager.RestartMouseSlot();
        Cursor.lockState = CursorLockMode.Locked;
    }


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
        if (PlayerManager.Instance.IsDead)
        {
            _playerInventoryPanel.SetActive(false);
            _craftingPanel.SetActive(false);
            DragDropManager.RestartMouseSlot();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void HandlerUIInput()
    {
        if (PlayerManager.Instance.IsDead) return;
        _playerInventoryPanel.SetActive(!_playerInventoryPanel.activeSelf);
        _craftingPanel.SetActive(!_craftingPanel.activeSelf);
        DragDropManager.RestartMouseSlot();
        Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.Confined : CursorLockMode.Locked;

    }

}
