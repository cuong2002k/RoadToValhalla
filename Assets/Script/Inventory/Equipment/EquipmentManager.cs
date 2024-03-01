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

    private CharacterMesh characterMesh;

    #endregion

    [SerializeField] private EquipmentItem[] _currentEquipment;
    [SerializeField] private SkinnedMeshRenderer[] _currentSkinnedMesh;

    // old item and new item
    public Action<EquipmentItem, EquipmentItem> OnChangeEquipmentItem;
    public Action<EquipmentItem, EquipmentItem> OnChangeUnEquipmentItem;


    private void Start()
    {
        int size = System.Enum.GetNames(typeof(EquipmentType)).Length;
        _currentEquipment = new EquipmentItem[size];
        _currentSkinnedMesh = new SkinnedMeshRenderer[size];
        characterMesh = GetComponent<CharacterMesh>();
    }

    public void Equip(EquipmentItem equipmentItem)
    {
        int index = (int)equipmentItem.GetEquipmentType();

        if (this._currentEquipment[index] != null)
        {
            EquipmentItem oldEquipment = _currentEquipment[index];
            Destroy(_currentSkinnedMesh[index].gameObject);
            _currentSkinnedMesh[index] = null;
            InventoryController.Instance.AddItem(new ItemStack(oldEquipment, 1));

            OnChangeEquipmentItem?.Invoke(null, oldEquipment);
        }
        this._currentEquipment[index] = equipmentItem;
        OnChangeEquipmentItem?.Invoke(equipmentItem, null);
        characterMesh.EquipSkinnedMesh(equipmentItem.GetEquipmentType(), equipmentItem.GetEquipmentMesh());

    }





}
