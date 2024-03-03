using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBarContainer : Container
{
    #region Singleton
    public static HotBarContainer Instance;
    #endregion
    private InputManager inputManager;
    private PlayerWeaponEquipment weponManager;
    public int Hotbar = 0;

    protected override void Start()
    {
        base.Start();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        inputManager = InputManager.Instance;
        weponManager = PlayerWeaponEquipment.Instance;
    }

    private void Update()
    {
        Hotbar = inputManager.HotBarInput;

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
                weponManager.UnEquipWeapon(weapon);
            }

        }
        else if (itemInSlot.GetItem() as WeaponConfig)
        {
            WeaponConfig weapon = itemInSlot.GetItem() as WeaponConfig;
            if (weapon.GetHandEquip() == HandEquip.LeftHand)
            {
                if (weponManager.HasItemLeftHand())
                {
                    CheckActiveItem(HandEquip.LeftHand);
                }
            }
            else if (weapon.GetHandEquip() == HandEquip.RightHand)
            {
                if (weponManager.HasItemRightHand())
                {
                    CheckActiveItem(HandEquip.RightHand);
                }
            }
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
                    weponManager.UnEquipWeapon(weapon);
                    break;
                }
            }
        }
    }
}
