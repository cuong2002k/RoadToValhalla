using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
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
        Initialize();
    }

    protected void Initialize()
    {
        _inventoryModel = new InventoryModel(_inventorySize);
        _inventoryPanel = new InventoryPanel(_inventorySize);
        _inventoryPanel.Initialize(_inventorySize, _inventoryPrefabs.transform);
        _inventoryModel.OnInventoryChange += this._inventoryPanel.RefestInventoryUI;

        for (int i = 0; i < _inventorySize; i++)
        {
            _inventoryPanel[i].SetInventory(this._inventoryModel);
            _inventoryPanel[i].OnClickAction += UIManager.Instance.OnClickHandler;
            _inventoryPanel[i].OnDropAction += UIManager.Instance.OnDropHandler;
        }
    }


}
