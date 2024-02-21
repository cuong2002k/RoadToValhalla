using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private InventoryModel _inventoryModel;
    [SerializeField] private InventoryPanel _inventoryPanel;
    [SerializeField] public GameObject InventoryPrefabs { get; private set; }
    private int _inventorySize = 25;

    public BaseItem item;

    private void Start()
    {
        InventoryPrefabs = this.gameObject;
        _inventoryModel = new InventoryModel(_inventorySize);
        _inventoryPanel = new InventoryPanel(_inventorySize);
        _inventoryModel.OnInventoryChange += this._inventoryPanel.RefestInventoryUI;

        _inventoryModel.AddItem(new ItemStack(item, 20));
        _inventoryModel.Invoke();
    }



}
