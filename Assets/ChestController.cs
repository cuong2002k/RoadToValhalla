using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public static ChestController Instance;
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
    public InventoryPanel _inventoryPanel;
    public InventoryModel _currentInventoryModel;
    public UI_DragDropManager _dragDropManager;
    public GameObject _inventoryPanelObject;
    public Transform SlotContainer;
    private void Start()
    {
        _dragDropManager = GetComponentInParent<UI_DragDropManager>();
        _inventoryPanel.Initialize(12, SlotContainer);
        for (int i = 0; i < 12; i++)
        {
            if (_dragDropManager != null)
            {
                _inventoryPanel[i].OnClickAction += _dragDropManager.OnClickHandler;
                _inventoryPanel[i].OnDropAction += _dragDropManager.OnDropHandler;
            }
        }
    }

    public void SetChestDisplay(int size, InventoryModel inventoryModel)
    {
        ClearChestUI();
        _currentInventoryModel = inventoryModel;
        _currentInventoryModel.OnInventoryChange += this._inventoryPanel.RefestInventoryUI;
        for (int i = 0; i < size; i++)
        {
            _inventoryPanel[i].SetInventory(this._currentInventoryModel);
        }
        Invoke();
    }

    private void ClearChestUI()
    {
        _currentInventoryModel.OnInventoryChange -= this._inventoryPanel.RefestInventoryUI;
    }

    public void Invoke()
    {
        _currentInventoryModel.Invoke();
    }


    public void OpenChest(bool active)
    {
        _inventoryPanelObject.SetActive(active);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenChest(false);
        }
    }
}
