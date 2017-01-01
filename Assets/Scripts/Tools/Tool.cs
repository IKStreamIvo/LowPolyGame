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

            if (!hit.GetComponent<WorldObject>())
            {
                while (hit.parent)
                {
                    if (hit.parent.GetComponent<WorldObject>())
                    {
                        hit = hit.parent;
                        break;
                    }

                    hit = hit.parent;
                }
            }

            if (hit.GetComponent<WorldObject>())
            {
                Debug.Log("LClick: " + hit.name);

                hit.GetComponent<WorldObject>().LClick(transform.parent.parent.gameObject);
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Transform hit = castRay(5f);

            if (!hit)
                return;
            if (hit.GetComponent<IgnoreCollider>())
                return;

            if (!hit.GetComponent<WorldObject>())
            {
                while (hit.parent)
                {
                    if (hit.parent.GetComponent<WorldObject>())
                    {
                        hit = hit.parent;
                        break;
                    }

                    hit = hit.parent;
                }
            }

            if (hit.GetComponent<WorldObject>())
            {
                Debug.Log("RClick: " + hit.name);

                hit.GetComponent<WorldObject>().RClick(transform.parent.parent.gameObject);
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
