using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Office : MonoBehaviour
{
    public float missionCompletion;
    public Image missionBar;
    public TextMeshProUGUI missionProgressText;
    public float missionCompletionRate;
    public GameObject prompt;
    private float missionLength = 100f;

    private void AdvanceMission()
    {
        missionCompletion += missionCompletionRate * Time.deltaTime;
        missionBar.fillAmount = missionCompletion / missionLength;
        missionProgressText.text = $"Completed {missionCompletion:F1}/{missionLength}";
        prompt.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        prompt.SetActive(true);
        if (Input.GetKey(KeyCode.E))
        {
            AdvanceMission();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        prompt.SetActive(false);
    }
}
