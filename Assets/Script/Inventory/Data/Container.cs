using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    private UI_DragDropManager _dragDropManager;

    #region Inventory
    // Inventory data
    [SerializeField] protected InventoryModel _inventoryModel;
    public InventoryModel InventoryModel => _inventoryModel;
    // Inventory UI
    [SerializeField] protected InventoryPanel _inventoryPanel;
    [SerializeField] protected GameObject _inventoryPrefabs;
    [SerializeField] protected int _inventorySize = 25;

    #endregion


    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        _inventoryPanel = GetComponentInChildren<InventoryPanel>();
        _dragDropManager = GetComponentInParent<UI_DragDropManager>();
        Initialize();
    }

    protected void Initialize()
    {
        _inventoryModel = new InventoryModel(_inventorySize);
        _inventoryPanel.Initialize(_inventorySize, _inventoryPrefabs.transform);
        _inventoryModel.OnInventoryChange += this._inventoryPanel.RefestInventoryUI;

        for (int i = 0; i < _inventorySize; i++)
        {
            _inventoryPanel[i].SetInventory(this._inventoryModel);
            if (_dragDropManager != null)
            {
                _inventoryPanel[i].OnClickAction += _dragDropManager.OnClickHandler;
                _inventoryPanel[i].OnDropAction += _dragDropManager.OnDropHandler;
            }
        }
    }

    public void Invoke()
    {
        _inventoryModel.Invoke();
    }
}
