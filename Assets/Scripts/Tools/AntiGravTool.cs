using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravTool : Tool {

    private bool _mouseState;
    private GameObject target;
    public Vector3 screenSpace;
    public Vector3 offset;

	public override void Update() {
        if (Input.GetButtonDown("Fire1"))
        {
            Transform hit = base.castRay(10f);

            // Check if there was actually a hit..
            if (!hit)
                return;
            if (hit.parent)
                if (hit.parent.GetComponent<ClickableObject>())
                    hit = hit.parent;

            if (hit.GetComponent<ClickableObject>())
            {
                if (hit.GetComponent<ClickableObject>().canPickedUp)
                {
                    // ANTIGRAV :D
                    _mouseState = true;
                    target = hit.gameObject;
                    target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    screenSpace = Camera.main.WorldToScreenPoint(target.transform.position);
                    offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
                }
            }
        }

        // MORE ANTIGRAV
        if (Input.GetMouseButtonUp(0))
        {
            _mouseState = false;
            if(target)
                target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }

        if (_mouseState)
        {
            //keep track of the mouse position
            var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

            //convert the screen mouse position to world point and adjust with offset
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

            //update the position of the object in the world
            target.transform.position = curPosition;
        }
    }
}
