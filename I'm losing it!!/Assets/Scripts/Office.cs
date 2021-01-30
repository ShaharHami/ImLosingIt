using UnityEngine;

public class Office : MonoBehaviour
{
    public GameObject prompt;
    public GameManager gameManager;
    private float missionLength = 100f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Player>() == null) return;
        prompt.SetActive(true);
        if (Input.GetKey(KeyCode.E))
        {
            gameManager.AdvanceMission();
            prompt.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() == null) return;
        prompt.SetActive(false);
    }
}
