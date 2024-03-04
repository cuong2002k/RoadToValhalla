using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHudManger : MonoBehaviour
{
    [SerializeField] private UI_StatsBar _UIStatsBar;

    public void SetValueStatsBar(int newValue)
    {
        this._UIStatsBar.SetStatsValue(newValue);
    }

    public void SetMaxStatsBar(int maxValue)
    {
        this._UIStatsBar.SetStatsValue(maxValue);
    }
}
