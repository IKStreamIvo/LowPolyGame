using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : WorldObject {

    public Vector3 collisionPoint;
	
	// Update is called once per frame
	void OnCollisionEnter (Collision c) {
        collisionPoint = c.contacts[0].point;
    }

    public override void LClick(GameObject source)
    {
        
    }

    public override void RClick(GameObject source)
    {
        
    }
}
