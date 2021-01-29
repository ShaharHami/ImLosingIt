using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    [HideInInspector] public float coolDownMeter = 0f;
    public float coolDownStep = 0.1f;
    public State state;
    public GameObject prompt;

    private ObstacleManager _obstacleManager;
    
    private void Awake()
    {
        _obstacleManager = FindObjectOfType<ObstacleManager>();
    }

    public enum State
    {
        Idle,
        Cooldown,
        Annoying
    }

    public virtual void Annoyed()
    {
        state = State.Annoying;
        coolDownMeter = 1f;
    }

    public virtual void Calm()
    {
        state = State.Idle;
        coolDownMeter = 0;
    }

    public virtual void CoolDown()
    {
        state = State.Cooldown;
        coolDownMeter -= coolDownStep * Time.deltaTime;
        if (coolDownMeter <= 0)
        {
            Calm();
            HidePrompt();
            _obstacleManager.canInteract = false;
        }
    }

    public virtual void ShowPrompt()
    {
        prompt.SetActive(true);
    }
    public virtual void HidePrompt()
    {
        prompt.SetActive(false);
    }
}
