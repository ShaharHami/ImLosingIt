using System.Collections;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    [HideInInspector] public float calmDownMeter = 0f;
    public float calmDownStep = 0.1f;
    public State state;
    public GameObject prompt;
    public float coolDownTime;
    public float damage;

    protected ObstacleManager _obstacleManager;
    
    private void Awake()
    {
        _obstacleManager = FindObjectOfType<ObstacleManager>();
    }

    public enum State
    {
        Idle,
        Calming,
        CoolDown,
        Annoying
    }

    public virtual void Annoyed()
    {
        state = State.Annoying;
        calmDownMeter = 1f;
    }

    public virtual void Calm()
    {
        state = State.Idle;
        calmDownMeter = 0;
    }

    public virtual void CalmDown()
    {
        state = State.Calming;
        calmDownMeter -= calmDownStep * Time.deltaTime;
        if (calmDownMeter <= 0)
        {
            HidePrompt();
            _obstacleManager.canInteract = false;
            CoolDown();
        }
    }

    public virtual void CoolDown()
    {
        StartCoroutine(CoolDownTimed());
    }
    
    private IEnumerator CoolDownTimed()
    {
        state = State.CoolDown;
        yield return new WaitForSeconds(coolDownTime);
        Calm();
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
