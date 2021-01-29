using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getaway : MonoBehaviour
{
    public float sanityBumpRate;
    private void OnTriggerStay2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            player.IncreaseSanity(sanityBumpRate);
        }
    }
}
