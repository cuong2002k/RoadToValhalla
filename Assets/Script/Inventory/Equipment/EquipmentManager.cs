using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EquipmentManager : MonoBehaviour
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
    }
    #endregion

    #region Equipment tranfrom
    [SerializeField] private Transform _bodyTranform;
    [SerializeField] private Transform _hipsTranform;
    #endregion
    [SerializeField] private EquipmentItem[] _currentEquipment;

    // old item and new item
    public Action<EquipmentItem, EquipmentItem> OnChangeEquipmentItem;
    public Action<EquipmentItem, EquipmentItem> OnChangeUnEquipmentItem;


    private void Start()
    {
        int size = System.Enum.GetNames(typeof(EquipmentType)).Length;
        _currentEquipment = new EquipmentItem[size];
    }

    public void Equip(EquipmentItem equipmentItem)
    {
        int index = (int)equipmentItem.GetEquipmentType();

        if (this._currentEquipment[index] != null)
        {
            EquipmentItem oldEquipment = _currentEquipment[index];
            InventoryController.Instance.AddItem(new ItemStack(oldEquipment, 1));
        }
        this._currentEquipment[index] = equipmentItem;

    }
}
