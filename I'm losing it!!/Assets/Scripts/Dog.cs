using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Obstacle
{
    public SpriteRenderer sprite;
    public override void Annoyed()
    {
        Debug.Log("Bark! Bark! Bark! Bark!");
        sprite.color = Color.red;
        base.Annoyed();
    }
    public override void Calm()
    {
        sprite.color = Color.white;
        base.Calm();
    }
    
    public override void CoolDown()
    {
        sprite.color = Color.Lerp(Color.white, Color.red, coolDownMeter);
        base.CoolDown();
    }
}
