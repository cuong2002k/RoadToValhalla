using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHudManger : MonoBehaviour
{
    [SerializeField] private UI_StatsBar _UIStataminaBar;
    [SerializeField] private UI_StatsBar _UIHealthbar;
    public void SetValueStatsBar(float newValue)
    {
        this._UIStataminaBar.SetStatsValue(newValue);
    }

    public void SetMaxStatsBar(float maxValue)
    {
        this._UIStataminaBar.SetMaxStats(maxValue);
    }
    public void SetValueHealthBar(float newValue)
    {
        this._UIHealthbar.SetStatsValue(newValue);
    }

    public void SetMaxHealthBar(float maxValue)
    {
        this._UIHealthbar.SetMaxStats(maxValue, false);
    }
}
