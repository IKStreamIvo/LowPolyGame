using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : ClickableObject {

    public Transform rails;

	// Use this for initialization
	void Start () {
        FindRails();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FindRails()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, .1f);
        foreach(Collider coll in hitColliders)
        {
            if (coll.name == "Rails")
            {
                Debug.Log(coll.name);
                rails = coll.transform;
            }
        }
    }
}
