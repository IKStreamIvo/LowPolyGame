using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRays : MonoBehaviour {

    public bool pickingUp;
    private bool _mouseState;
    private GameObject target;
    public Vector3 screenSpace;
    public Vector3 offset;

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Transform hit = castRay(10f);

            // Check if there was actually a hit..
            if (!hit)
                return;
            if (hit.parent)
                if (hit.parent.GetComponent<ClickableObject>())
                    hit = hit.parent;

            if (hit.GetComponent<ClickableObject>())
            {
                if (!pickingUp)
                {
                    Debug.Log("LClick: " + hit.name);

                    hit.GetComponent<ClickableObject>().LClick(gameObject);
                }
                else if(pickingUp && hit.GetComponent<ClickableObject>().canPickedUp)
                {
                    // ANTIGRAV :D
                    _mouseState = true;
                    target = hit.gameObject;
                    screenSpace = Camera.main.WorldToScreenPoint(target.transform.position);
                    offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
                }
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Transform hit = castRay(10f);

            if (!hit)
                return;
            if (hit.parent)
                if (hit.parent.GetComponent<ClickableObject>())
                    hit = hit.parent;
            if (hit.GetComponent<ClickableObject>())
            {
                Debug.Log("RClick: " + hit.name);

                hit.GetComponent<ClickableObject>().RClick(gameObject);
            }
        }

        // MORE ANTIGRAV
        if (Input.GetMouseButtonUp(0))
            _mouseState = false;

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

    Transform castRay(float dist)
    {
        RaycastHit hit;
        Ray ray = transform.FindChild("Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 15f))
        {
            return hit.transform;
        }
        else
            return null;
    }
}
