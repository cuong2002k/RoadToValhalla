using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHudManger : MonoBehaviour
{
    [SerializeField] private UI_StatsBar _UIStatsBar;
    public void SetValueStatsBar(float newValue)
    {
        this._UIStatsBar.SetStatsValue(newValue);
    }

    public void SetMaxStatsBar(float maxValue)
    {
        this._UIStatsBar.SetMaxStats(maxValue);
    }
}
