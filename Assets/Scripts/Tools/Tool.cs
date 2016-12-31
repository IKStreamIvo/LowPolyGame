using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour {

    public string toolType;
    public bool swingAnim;
    // public float durability;

    public virtual void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Transform hit = castRay(5f);

            if (!hit)
                return;
            if (hit.GetComponent<IgnoreCollider>())
                return;

            if (!hit.GetComponent<ClickableObject>())
            {
                while (hit.parent)
                {
                    if (hit.parent.GetComponent<ClickableObject>())
                    {
                        hit = hit.parent;
                        break;
                    }

                    hit = hit.parent;
                }
            }

            if (hit.GetComponent<ClickableObject>())
            {
                Debug.Log("LClick: " + hit.name);

                hit.GetComponent<ClickableObject>().LClick(transform.parent.parent.gameObject);
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Transform hit = castRay(5f);

            if (!hit)
                return;
            if (hit.GetComponent<IgnoreCollider>())
                return;

            while (hit.parent)
            {
                if (hit.parent.GetComponent<ClickableObject>())
                {
                    hit = hit.parent;
                    break;
                }

                hit = hit.parent;
            }

            if (hit.GetComponent<ClickableObject>())
            {
                Debug.Log("RClick: " + hit.name);

                hit.GetComponent<ClickableObject>().RClick(transform.parent.parent.gameObject);
            }
        }
    }

    public Transform castRay(float dist)
    {
        RaycastHit hit;
        Ray ray = transform.parent.parent.FindChild("Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (GetComponent<Animation>()) {
            if (!GetComponent<Animation>().isPlaying) {
                if (swingAnim)
                    GetComponent<Animation>().Play("ToolSwing");
            }
            else
                return null;
        }

        if (Physics.Raycast(ray, out hit, dist))
            return hit.transform;
        else
            return null;
    }
}
