using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Baby : Obstacle
{
    public Image bar;
    
    private void Start()
    {
        bar.fillAmount = 0;
    }
    public override void Annoyed()
    {
        Debug.Log("Baby is not amused");
        bar.fillAmount = 1;
        base.Annoyed();
    }

    public override void Calm()
    {
        bar.fillAmount = 0;
        base.Calm();
    }

    public override void CalmDown()
    {
        bar.fillAmount = calmDownMeter;
        base.CalmDown();
    }
}
