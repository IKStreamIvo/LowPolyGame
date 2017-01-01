using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmelterDoor : WorldObject {

    public bool open = false;
    
    public override void RClick(GameObject source)
    {
        if (!open && !transform.parent.GetComponent<Animation>().isPlaying)
        {
            transform.parent.GetComponent<Animation>()["DoorOpen"].speed = 1;
            transform.parent.GetComponent<Animation>().Play("DoorOpen");
            open = !open;
        }
        else if (open && !transform.parent.GetComponent<Animation>().isPlaying)
        {
            transform.parent.GetComponent<Animation>()["DoorOpen"].speed = -1;
            transform.parent.GetComponent<Animation>()["DoorOpen"].time = transform.parent.GetComponent<Animation>()["DoorOpen"].length;
            transform.parent.GetComponent<Animation>().Play("DoorOpen");
            open = !open;
        }
    }
}
