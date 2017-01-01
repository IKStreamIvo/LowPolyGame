using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCapsule : WorldObject {

    private bool open = false;

    public override void LClick(GameObject source)
    {
    }

    public override void RClick(GameObject source)
    {
        if (!open && !GetComponent<Animation>().isPlaying)
        {
            GetComponent<Animation>()["DoorOpens"].speed = 1;
            GetComponent<Animation>().Play("DoorOpens");
            open = !open;
        }
        else if(open && !GetComponent<Animation>().isPlaying)
        {
            GetComponent<Animation>()["DoorOpens"].speed = -1;
            GetComponent<Animation>()["DoorOpens"].time = GetComponent<Animation>()["DoorOpens"].length;
            GetComponent<Animation>().Play("DoorOpens");
            open = !open;
        }
    }
}
