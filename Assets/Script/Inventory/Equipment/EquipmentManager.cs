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

    private InventoryController _inventoryController;

    #region Equipment tranfrom

    private CharacterMesh characterMesh;

    #endregion

    [SerializeField] private EquipmentItem[] _currentEquipment;
    [SerializeField] private SkinnedMeshRenderer[] _currentSkinnedMesh;

    // Event to change Stats modifier when change equipment item
    public Action<EquipmentItem, EquipmentItem> OnChangeEquipmentItem;
    public Action<EquipmentItem, EquipmentItem> OnChangeUnEquipmentItem;


    private void Start()
    {
        int size = System.Enum.GetNames(typeof(EquipmentType)).Length;
        _currentEquipment = new EquipmentItem[size];
        _currentSkinnedMesh = new SkinnedMeshRenderer[size];
        characterMesh = GetComponent<CharacterMesh>();
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
            Destroy(_currentSkinnedMesh[index].gameObject);
            _currentSkinnedMesh[index] = null;
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
        characterMesh.EquipSkinnedMesh(equipmentItem.GetEquipmentType(), equipmentItem.GetEquipmentMesh());

    }





}
