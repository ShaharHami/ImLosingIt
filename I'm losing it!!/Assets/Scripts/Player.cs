using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public float sanity;
    public float baseErosionRate;
    public Image sanityBar;
    public TextMeshProUGUI sanityLevel;
    public SpriteRenderer faceRenderer;
    public Sprite[] moods;

    public void ErodeSanity(List<Obstacle> obstacles)
    {
        sanity -= ErosionAmount(obstacles) * baseErosionRate * Time.deltaTime;
        UpdateUI();
        if (sanity <= 0)
        {
            sanity = 0;
            gameManager.GameOver();
        }
    }

    private float ErosionAmount(List<Obstacle> obstacles)
    {
        return obstacles.Sum(obstacle => obstacle.damage);
    }
    
    public void IncreaseSanity(float rate)
    {
        if (sanity <= 100)
        {
            sanity += rate * Time.deltaTime;
            UpdateUI();
        }
    }

    private void UpdateUI()
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
