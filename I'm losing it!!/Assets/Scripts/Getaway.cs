using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getaway : MonoBehaviour
{
    public float sanityBumpRate;
    public GameObject prompt;
    private void OnTriggerStay2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            prompt.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                player.IncreaseSanity(sanityBumpRate);
                prompt.SetActive(false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        prompt.SetActive(false);
    }
}
