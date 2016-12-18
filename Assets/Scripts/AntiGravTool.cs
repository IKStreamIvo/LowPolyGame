using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravTool : Tool {

	// Use this for initialization
	void Start () {
        if (transform.parent)
            if (transform.parent.name.Contains("Hand"))
                transform.parent.parent.GetComponent<PlayerRays>().pickingUp = true;
    }

    void OnDestroy()
    {
        if (transform.parent)
            if (transform.parent.name.Contains("Hand"))
                transform.parent.parent.GetComponent<PlayerRays>().pickingUp = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
