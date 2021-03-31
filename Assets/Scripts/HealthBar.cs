using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void ReduceSliderValue(float amount)
    {
        StartCoroutine(ReduceSliderValueRoutine(amount));
    }

    IEnumerator ReduceSliderValueRoutine(float amount)
    {
        float target = slider.value - amount;
        while (slider.value > target)
        {
            yield return new WaitForSeconds(0.05f);
            slider.value -= 0.5f;
        }       
    }
}
