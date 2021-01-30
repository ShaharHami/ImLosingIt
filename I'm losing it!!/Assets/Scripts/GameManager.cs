using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public ObstacleManager obstacleManager;
    public Player player;
    public float missionCompletion;
    public Image missionBar;
    public TextMeshProUGUI missionProgressText;
    public float missionCompletionRate;
    public float difficultyIncreaseDelay;
    private float minCheckDelay, maxObstacleProbability, maxObstacles;
    private float missionLength = 100f;

    private void Start()
    {
        StartCoroutine(Difficulty());
        minCheckDelay = 0.5f;
        maxObstacleProbability = 0.85f;
        maxObstacles = obstacleManager.obstacles.Count;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        obstacleManager.gameOver = true;
        StopAllCoroutines();
        Debug.Log("You Lose");
    }

    public void WinGame()
    {
        Time.timeScale = 0;
        obstacleManager.gameOver = true;
        StopAllCoroutines();
        Debug.Log("You Win");
    }
    
    public void AdvanceMission()
    {
        if (missionCompletion < missionLength)
        {
            missionCompletion += missionCompletionRate * Time.deltaTime;
            missionBar.fillAmount = missionCompletion / missionLength;
            missionProgressText.text = $"Completed {missionCompletion:F1}/{missionLength}";
        }
        else
        { 
           missionProgressText.text = $"Completed {missionLength}/{missionLength}";
           WinGame(); 
        }
    }

    private IEnumerator Difficulty()
    {
        while (true)
        {
            yield return new WaitForSeconds(difficultyIncreaseDelay);
            if (obstacleManager.maxObstacles < maxObstacles && Random.Range(0f, 1f) > 0.5f)
            {
                obstacleManager.maxObstacles++;
            }

            if (obstacleManager.obstacleProbability < maxObstacleProbability)
            {
                obstacleManager.obstacleProbability += 0.03f;
            }

            if (obstacleManager.checkInterval > minCheckDelay)
            {
                obstacleManager.checkInterval -= 0.4f;
            }
        }
    }
}
