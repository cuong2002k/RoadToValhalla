using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class StatsModifield
{
    [SerializeField]
    private int _baseStats;
    [SerializeField]
    private List<int> modifiedBonus = new List<int>();
    public int GetStatsValue()
    {
        int finalStats = this._baseStats;
        modifiedBonus.ForEach(value => finalStats += value);
        return finalStats;
    }

    public void AddModified(int valueToAdd)
    {
        if (valueToAdd > 0)
            modifiedBonus.Add(valueToAdd);
    }

    public void RemoveModified(int valueToRemove)
    {
        if (valueToRemove > 0)
            modifiedBonus.Remove(valueToRemove);
    }
}
