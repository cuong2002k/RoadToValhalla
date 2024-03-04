using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public class UI_StatsBar : MonoBehaviour
{
    private Slider _statsSlider;
    private void Start()
    {
        _statsSlider = GetComponent<Slider>();
    }

    // update value
    public void SetStatsValue(int newValue)
    {
        _statsSlider.value = newValue;
    }
    // set max value
    public void SetMaxStats(int maxValue)
    {
        _statsSlider.maxValue = maxValue;
    }
}
