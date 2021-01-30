using UnityEngine;
using UnityEngine.UI;

public class Kid : Obstacle
{
    public NPCController npcController;
    private bool audioStopped;
    private void Start()
    {
        bar.fillAmount = 0;
    }

    public override void Annoyed()
    {
        npcController.StartFollowing();
        audioStopped = false;
        source.clip = annoySfx;
        source.loop = true;
        source.Play();
        base.Annoyed();
    }

    public override void ShowPrompt()
    {
        if (Vector3.Distance(npcController._transform.position, npcController.target.transform.position) < 2f)
        {
            base.ShowPrompt();
        }
    }

    public override void CalmDown()
    {
        HidePrompt();
        if (Vector3.Distance(npcController._transform.position, npcController.target.transform.position) < 2f)
        {
            ShowPrompt();
            bar.fillAmount = calmDownMeter;
            if (!audioStopped)
            {
                source.clip = null;
                source.loop = false;
                source.Stop();
                source.PlayOneShot(calmSfx);
                audioStopped = true;
            }
            base.CalmDown();
        }
    }

    public override void CoolDown()
    {
        npcController.StopFollowing();
        base.CoolDown();
    }
}
