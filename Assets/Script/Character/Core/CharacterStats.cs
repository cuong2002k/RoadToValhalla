using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] StatsModifield _maxHp;
    [SerializeField] StatsModifield _damage;
    [SerializeField] StatsModifield _defense;

    private void Start()
    {
        EquipmentManager.Instance.OnChangeEquipmentItem += UpdateModified;
    }

    private void UpdateModified(EquipmentItem newItem, EquipmentItem oldItem)
    {
        if (newItem != null)
        {
            _damage.AddModified(newItem.GetAttackModified());
            _defense.AddModified(newItem.GetArmorModifier());
        }

        if (oldItem != null)
        {
            _damage.RemoveModified(oldItem.GetAttackModified());
            _defense.RemoveModified(oldItem.GetArmorModifier());
        }

        // Debug.Log(_damage.GetStatsValue() + " " + _defense.GetStatsValue());
    }

    
}
