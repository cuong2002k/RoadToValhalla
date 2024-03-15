using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EquipmentManager : MonoBehaviour, IBind<EquipmentData>
{
    #region Singleton
    public static EquipmentManager Instance;
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
        _characterMesh = GetComponent<CharacterMesh>();
        int size = System.Enum.GetNames(typeof(EquipmentType)).Length;
        _currentEquipment = new EquipmentItem[size];
    }
    #endregion

    private InventoryController _inventoryController;

    #region Equipment tranfrom

    private CharacterMesh _characterMesh;
    [SerializeField] private EquipmentData _equipmentData = new EquipmentData();
    #endregion
    public string id { get; set; } = System.Guid.NewGuid().ToString();

    [SerializeField] private EquipmentItem[] _currentEquipment;

    // Event to change Stats modifier when change equipment item
    public Action<EquipmentItem, EquipmentItem> OnChangeEquipmentItem;
    public Action<EquipmentItem, EquipmentItem> OnChangeUnEquipmentItem;

    private void Start()
    {
        _inventoryController = PlayerUIManager.Instance.InventoryController;
    }

    // equipment item
    public void Equip(EquipmentItem equipmentItem)
    {
        // get index with id of enum
        int index = (int)equipmentItem.GetEquipmentType();
        //item contains in slot => destroy this
        if (this._currentEquipment[index] != null)
        {
            EquipmentItem oldEquipment = _currentEquipment[index];
            //add item to inventory
            _inventoryController.AddItem(new ItemStack(oldEquipment, 1));

            // call event update stats
            OnChangeEquipmentItem?.Invoke(null, oldEquipment);
        }
        // set current equipment
        this._currentEquipment[index] = equipmentItem;
        // call event update stats
        OnChangeEquipmentItem?.Invoke(equipmentItem, null);
        // Update Mesh
        _characterMesh.EquipSkinnedMesh(equipmentItem.GetEquipmentType(), equipmentItem.GetEquipmentMesh());
    }

    public void Bind(EquipmentData data)
    {
        _equipmentData = data;
        bool isNew = _equipmentData.EquipData == null || _equipmentData == null;
        if (isNew)
        {
            int size = System.Enum.GetNames(typeof(EquipmentType)).Length;
            _equipmentData.EquipData = new EquipmentItem[size];
        }
        else
        {
            for (int i = 0; i < _equipmentData.EquipData.Length; i++)
            {
                if (_equipmentData.EquipData[i] == null) continue;
                EquipmentItem equipmentItem = ItemDatabase.GetItemWithID(_equipmentData.EquipData[i].ID) as EquipmentItem;
                if (equipmentItem != null)
                {
                    Equip(equipmentItem);
                    _equipmentData.EquipData[i] = equipmentItem;
                }

            }
        }
        this._currentEquipment = _equipmentData.EquipData;
    }
}

