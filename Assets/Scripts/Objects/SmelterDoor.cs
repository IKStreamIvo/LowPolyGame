using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmelterDoor : WorldObject {

    public bool open = false;
    public bool locked = false;
    private GameObject smelter;
    
    void Start()
    {
        smelter = transform.parent.gameObject;
    }

    public override void RClick(GameObject source)
    {
        ToggleDoor();
    }

    public void ToggleDoor()
    {
        if (locked)
            return;

        if (!open && !smelter.GetComponent<Animation>().isPlaying)
        {
            smelter.GetComponent<Animation>()["DoorOpen"].speed = 1;
            smelter.GetComponent<Animation>().Play("DoorOpen");
            open = !open;
        }
        else if (open && !smelter.GetComponent<Animation>().isPlaying)
        {
            smelter.GetComponent<Animation>()["DoorOpen"].speed = -1;
            smelter.GetComponent<Animation>()["DoorOpen"].time = smelter.GetComponent<Animation>()["DoorOpen"].length;
            smelter.GetComponent<Animation>().Play("DoorOpen");
            open = !open;
        }
    }
}
