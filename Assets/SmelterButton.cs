using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmelterButton : WorldObject {

    private MolecularSmelter smelter;

	// Use this for initialization
	void Start () {
        smelter = transform.parent.GetComponent<MolecularSmelter>();
	}

    public override void RClick(GameObject source)
    {
        smelter.Activate();
    }
}
