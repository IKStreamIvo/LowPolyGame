using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmelterButton : ClickableObject {

    public override void LClick(GameObject source)
    {
        
    }

    public override void RClick(GameObject source)
    {
        transform.parent.GetComponent<Animation>().Play("DoorOpen");
    }
}
