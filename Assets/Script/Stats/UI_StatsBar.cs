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
    private float newValue = 0;
    private float velocity;

    private void Awake()
    {
        _statsSlider = GetComponent<Slider>();
        _rectranform = GetComponent<RectTransform>();

    }

    private void Update()
    {
        float valueChange = Mathf.SmoothDamp(_statsSlider.value, newValue, ref velocity, 10 * Time.deltaTime);
        _statsSlider.value = valueChange;
    }

    // update value
    public void SetStatsValue(float newValue)
    {
        this.newValue = newValue;
        // StartCoroutine(SmoothSlide(newValue));
    }

    // set max value
    public void SetMaxStats(float maxValue, bool width = true)
    {
        _statsSlider.maxValue = maxValue;
        _statsSlider.value = maxValue;
        newValue = maxValue;
        Vector2 newSize = width ? new Vector2(maxValue * 2, _rectranform.sizeDelta.y) : new Vector2(_rectranform.sizeDelta.x, maxValue * 2);
        _rectranform.sizeDelta = newSize;
    }

    IEnumerator SmoothSlide(float newvalue)
    {
        float currentTime = 0;
        while (currentTime < _timeChangeValue)
        {
            float currentStamina = _statsSlider.value;
            float value = Mathf.Lerp(currentStamina, newvalue, currentTime / _timeChangeValue);
            _statsSlider.value = value;
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
