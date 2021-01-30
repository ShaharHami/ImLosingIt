using UnityEngine;
using UnityEngine.UI;

public class Kid : Obstacle
{
    public NPCController npcController;
    public Image bar;

    private void Start()
    {
        bar.fillAmount = 0;
    }

    public override void Annoyed()
    {
        npcController.StartFollowing();
        bar.fillAmount = 1;
        base.Annoyed();
    }

    public override void ShowPrompt()
    {
        if (Vector3.Distance(npcController._transform.position, npcController.target.transform.position) < 1f)
        {
            base.ShowPrompt();
        }
    }

    public override void CalmDown()
    {
        HidePrompt();
        if (Vector3.Distance(npcController._transform.position, npcController.target.transform.position) < 1f)
        {
            ShowPrompt();
            bar.fillAmount = calmDownMeter;
            base.CalmDown();
        }
    }

    public override void CoolDown()
    {
        npcController.StopFollowing();
        base.CoolDown();
    }
}
