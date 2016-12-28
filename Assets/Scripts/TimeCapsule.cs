using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCapsule : ClickableObject {

    private bool open = false;

    public override void LClick(GameObject source)
    {
    }

    public override void RClick(GameObject source)
    {
        if (!open)
        {
            gameObject.GetComponent<Animation>().Play("DoorOpens");
            open = !open;
        }
    }
}
