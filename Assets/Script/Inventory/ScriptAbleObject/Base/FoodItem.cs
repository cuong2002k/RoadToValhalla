using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : BaseItem
{
    private int _health;
    private int _stamina;
    private void Start()
    {
        this._itemType = ItemType.FoodItem;
    }
    public int Health { get => _health; set => _health = value; }
    public int Stamina { get => _stamina; set => _stamina = value; }

}
