using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ObstacleManager obstacleManager;
    public Player player;
    public float missionCompletion;
    public Image missionBar;
    public TextMeshProUGUI missionProgressText;
    public float missionCompletionRate;
    private float missionLength = 100f;
    
    public void GameOver()
    {
        Time.timeScale = 0;
        obstacleManager.gameOver = true;
        Debug.Log("You Lose");
    }

    public void WinGame()
    {
        Time.timeScale = 0;
        obstacleManager.gameOver = true;
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
}
