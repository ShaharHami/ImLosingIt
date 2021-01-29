using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject opendoor;
    public GameObject closeddoor;

    public bool startOpen = false;
    private bool isOpen;

    private void Start()
    {
        if (startOpen)
            OpenDoor();
        else
        {
            CloseDoor();
        }

    }

    public void Interact()
    {
        if(isOpen)
            CloseDoor();
        else
        {
            OpenDoor();
        }
    }
    public void CloseDoor()
    {
        opendoor.SetActive(false);
        closeddoor.SetActive(true);
        isOpen = false;
    }
    public void OpenDoor()
    {
        opendoor.SetActive(true);
        closeddoor.SetActive(false);
        isOpen = true;
    }
}
