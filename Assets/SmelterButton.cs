using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmelterButton : WorldObject {

    public bool onbutton;
    public bool ejectbutton;

    private MolecularSmelter smelter;

	// Use this for initialization
	void Start () {
        smelter = transform.parent.GetComponent<MolecularSmelter>();
	}

    public override void RClick(GameObject source)
    {
        if (onbutton)
            StartCoroutine(smelter.Activate());
        else if (ejectbutton)
            StartCoroutine(smelter.Eject());
    }
}
