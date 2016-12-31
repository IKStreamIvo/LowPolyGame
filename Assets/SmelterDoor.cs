using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmelterDoor : ClickableObject {

    private bool open = false;


    public override void RClick(GameObject source)
    {
        if (!open && !GetComponent<Animation>().isPlaying)
        {
            GetComponent<Animation>()["DoorOpen"].speed = 1;
            GetComponent<Animation>().Play("DoorOpen");
            open = !open;
        }
        else if (open && !GetComponent<Animation>().isPlaying)
        {
            GetComponent<Animation>()["DoorOpen"].speed = -1;
            GetComponent<Animation>()["DoorOpen"].time = GetComponent<Animation>()["DoorOpen"].length;
            GetComponent<Animation>().Play("DoorOpen");
            open = !open;
        }
    }
}
