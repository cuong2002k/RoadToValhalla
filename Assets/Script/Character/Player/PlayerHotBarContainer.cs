using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHotBarContainer : Container, IBind<InventoryData>
{
    public string id { get; set; } = System.Guid.NewGuid().ToString();
    private PlayerWeaponEquipment _weponManager;
    public int Hotbar = 0;

    protected override void Start()
    {
        base.Start();
        _weponManager = PlayerManager.Instance.PlayerWeaponEquipment;
        // if (PlayerUIManager.Instance != null)

    }

    public void UseSlot(int index)
    {
        ItemStack itemInSlot = this._inventoryModel[index - 1];
        if (itemInSlot.GetActive())
        {
            itemInSlot.SetActive(false);
            if (itemInSlot.GetItem() as WeaponConfig)
            {
                WeaponConfig weapon = itemInSlot.GetItem() as WeaponConfig;
                _weponManager.UnEquipWeapon(weapon);
            }
        }
        else if (itemInSlot.GetItem() as WeaponConfig)
        {
            WeaponConfig weapon = itemInSlot.GetItem() as WeaponConfig;
            if (_weponManager.HasItemLeftHand() && weapon.GetHandEquip() == HandEquip.LeftHand)
            {
                CheckActiveItem(HandEquip.LeftHand);
            }
            else if (_weponManager.HasItemRightHand() && weapon.GetHandEquip() == HandEquip.RightHand)
            {
                CheckActiveItem(HandEquip.RightHand);
            }

            itemInSlot.Equip();
        }
        else
        {
            itemInSlot.Equip();
        }
        this.Invoke();

    }


    public void CheckActiveItem(HandEquip handEquip)
    {
        for (int i = 0; i < this._inventorySize; i++)
        {
            ItemStack slotBar = this._inventoryModel[i];
            if (slotBar.IsEmpty()) continue;
            if (slotBar.GetActive() && (slotBar.GetItem() as WeaponConfig))
            {
                WeaponConfig weapon = (slotBar.GetItem() as WeaponConfig);
                if (weapon.GetHandEquip() == handEquip)
                {
                    slotBar.SetActive(false);
                    _weponManager.UnEquipWeapon(weapon);
                    break;
                }
            }
        }
    }

    public void Bind(InventoryData data)
    {
        this._inventoryModel.Bind(data, this._inventorySize);
    }
}
