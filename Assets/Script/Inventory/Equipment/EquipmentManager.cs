using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EquipmentManager : MonoBehaviour, ISaveData
{
    public string id { get; set; }
    private InventoryController _inventoryController;

    #region Equipment tranform

    private CharacterMesh _characterMesh;

    #endregion
    [SerializeField] private EquipmentItem[] _currentEquipment;

    // Event to change Stats modifier when change equipment item
    public Action<EquipmentItem, EquipmentItem> OnChangeEquipmentItem;
    public Action<EquipmentItem, EquipmentItem> OnChangeUnEquipmentItem;

    private void Awake()
    {
        _characterMesh = GetComponent<CharacterMesh>();
        int size = Enum.GetNames(typeof(EquipmentType)).Length;
        _currentEquipment = new EquipmentItem[size];
    }

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

    public object CaptureState()
    {
        string[] equipmentData = new string[_currentEquipment.Length];
        for (int i = 0; i < _currentEquipment.Length; i++)
        {
            if (_currentEquipment[i] != null)
            {
                equipmentData[i] = _currentEquipment[i].ID;
            }
        }
        return equipmentData;
    }

    public void RestoreState(object state)
    {
        string[] equipmentData = (string[])state;
        foreach (string itemId in equipmentData)
        {
            if (itemId != null)
            {
                EquipmentItem item = ItemDatabase.GetItemWithID(itemId) as EquipmentItem;
                Equip(item);
            }
        }

    }
}

