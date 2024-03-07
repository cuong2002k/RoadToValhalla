using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public class UI_StatsBar : MonoBehaviour
{
    private Slider _statsSlider;
    private RectTransform _rectranform;
    private float _timeChangeValue = 0.5f;

    private void Awake()
    {
        _statsSlider = GetComponent<Slider>();
        _rectranform = GetComponent<RectTransform>();

    }


    // update value
    public void SetStatsValue(float newValue)
    {
        StartCoroutine(SmoothSlide(newValue));
    }

    // set max value
    public void SetMaxStats(float maxValue)
    {
        _statsSlider.maxValue = maxValue;
        _statsSlider.value = maxValue;
        _rectranform.sizeDelta = new Vector2(maxValue * 2, _rectranform.sizeDelta.y);
    }

    IEnumerator SmoothSlide(float newvalue)
    {
        float currentTime = 0;
        while (currentTime < _timeChangeValue)
        {
            yield return new WaitForEndOfFrame();
            float currentStamina = _statsSlider.value;
            float value = Mathf.Lerp(currentStamina, newvalue, currentTime / _timeChangeValue);
            _statsSlider.value = value;
            currentTime += Time.deltaTime;
        }
    }
}
