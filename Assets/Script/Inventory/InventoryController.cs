using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class InventoryController : MonoBehaviour
{
    #region Singleton
    public static InventoryController Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }
    #endregion

    #region Inventory
    // Inventory data
    [SerializeField] private InventoryModel _inventoryModel;
    // Inventory UI
    [SerializeField] private InventoryPanel _inventoryPanel;
    [SerializeField] public GameObject InventoryPrefabs { get; private set; }
    private int _inventorySize = 25;
    public BaseItem item;
    #endregion




    private void Start()
    {
        InventoryPrefabs = this.gameObject;
        _inventoryModel = new InventoryModel(_inventorySize);
        _inventoryPanel = new InventoryPanel(_inventorySize);
        _inventoryModel.OnInventoryChange += this._inventoryPanel.RefestInventoryUI;
        _inventoryModel.AddItem(new ItemStack(item, 20));
        _inventoryModel.Invoke();

        for (int i = 0; i < _inventorySize; i++)
        {
            _inventoryPanel[i].SetInventory(this._inventoryModel);
            _inventoryPanel[i].OnClickAction += UIManager.Instance.OnClickHandler;
            _inventoryPanel[i].OnDropAction += UIManager.Instance.OnDropHandler;
        }

    }






}
