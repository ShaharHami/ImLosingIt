using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNightLight : MonoBehaviour
{
    public Light2D globalLight;
    public Color dayColor;
    public Color nightColor;
    public float dayIntensity;
    public float nightIntensity;
    public float dayTime, nightTime;
    private float step;
    private bool stopCycle;

    private void Start()
    {
        step = 0.01f;
    }

    private void Update()
    {
        Debug.Log(stopCycle);
        if (!stopCycle)
        {
            ChangeIntensity(step);
        }
        globalLight.color = Color.Lerp(nightColor, dayColor, globalLight.intensity);
        if (globalLight.intensity >= dayIntensity)
        {
            StartCoroutine(PeriodDelay(dayTime, -0.01f));
        }
        else if (globalLight.intensity <= nightIntensity)
        {
            
            StartCoroutine(PeriodDelay(nightTime, 0.01f));
        }
    }

    private IEnumerator PeriodDelay(float duration, float i)
    {
        stopCycle = true;
        yield return new WaitForSeconds(duration);
        stopCycle = false;
        step = i;
    }
    
    private void ChangeIntensity(float changeRate)
    {
        globalLight.intensity += changeRate * Time.deltaTime;
    }
}
