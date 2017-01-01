using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolecularSmelter : WorldObject {

    // Default stuff
    private SmelterDoor door;
    void Start ()
    {
        door = transform.FindChild("Armature").GetComponent<SmelterDoor>();
    }

    // Click Managers
    public override void RClick(GameObject source)
    {
        if (door.open)
        {
            // Play blocked-sound?
            return;
        }
        else
        {
            Debug.Log("Smelt!");
            Smelt();
        }
    }

    // Smelting
    public List<GameObject> smeltables;
    public List<GameObject> unsmeltables;

    void OnTriggerEnter(Collider coll)
    {
        Transform obj = coll.transform;

        if (!obj.GetComponent<WorldObject>())
        {
            while (obj.parent)
            {
                if (obj.parent.GetComponent<WorldObject>())
                {
                    obj = obj.parent;
                    break;
                }

                obj = obj.parent;
            }
        }

        if (obj.gameObject.GetComponent<WorldObject>().smeltable)
            smeltables.Add(obj.gameObject);
        else
            unsmeltables.Add(obj.gameObject);
    }

    void OnTriggerExit(Collider coll)
    {
        Transform obj = coll.transform;

        if (!obj.GetComponent<WorldObject>())
        {
            while (obj.parent)
            {
                if (obj.parent.GetComponent<WorldObject>())
                {
                    obj = obj.parent;
                    break;
                }

                obj = obj.parent;
            }
        }

        if (smeltables.Contains(obj.gameObject))
            smeltables.Remove(obj.gameObject);
        else if (unsmeltables.Contains(obj.gameObject))
            unsmeltables.Remove(obj.gameObject);
    }

    void Smelt()
    {
        foreach (GameObject ore in smeltables)
        {
            //smeltables.Remove(ore);
            GameObject ingot = (GameObject)Instantiate(ore.GetComponent<WorldObject>().smeltedForm, ore.transform.position + new Vector3(0, .5f, 0), Quaternion.identity);
            Destroy(ore);
        }
    }
}
