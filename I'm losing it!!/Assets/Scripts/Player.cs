using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float sanity;
    public float baseErosionRate;
    public Image sanityBar;
    public TextMeshProUGUI sanityLevel;
    public SpriteRenderer faceRenderer;
    public Sprite[] moods;

    public void ErodeSanity(int obstacles)
    {
        sanity -= obstacles * baseErosionRate * Time.deltaTime;
    }

    public void IncreaseSanity(float rate)
    {
        if (sanity <= 100)
        {
            sanity += rate * Time.deltaTime;
        }
    }

    private void Update()
    {
        sanityBar.fillAmount = sanity / 100;
        sanityLevel.text = $"Sanity {sanity:F1}/100";
    }

    public void changeMood(int moodIdx)
    {
        if (moodIdx < 0 || moodIdx >= moods.Length)
            faceRenderer.gameObject.SetActive(false);
        else
        {
            faceRenderer.gameObject.SetActive(true);
            faceRenderer.sprite = moods[moodIdx];
        }
    }
}
