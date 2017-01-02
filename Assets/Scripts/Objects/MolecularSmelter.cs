using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolecularSmelter : WorldObject {

    // Default stuff
    public SmelterDoor door;
    void Start ()
    {
        door = gameObject.transform.FindChild("Armature").GetComponent<SmelterDoor>();
    }

    // Click Managers
    public override void RClick(GameObject source)
    {
        Debug.Log(door.open);
        if (door.open)
        {
            // Play blocked-sound?
            return;
        }
        else
        {
            //Debug.Log("Smelt!");
            //Smelt();
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
        //if (ejecting)
            //return;
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

    public void Activate()
    {
        Debug.Log("Smelt!");
        door.locked = true;
        foreach (GameObject ore in smeltables)
        {
            GameObject ingot = (GameObject)Instantiate(ore.GetComponent<WorldObject>().smeltedForm, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            Destroy(ore);

        }
        smeltables.Clear();
        door.locked = false;
    }

    public bool ejecting;
    public IEnumerator Eject()
    {
        //ejecting = true;
        door.ToggleDoor();
        // Wait for the door to be opened
        float time = GetComponent<Animation>()["DoorOpen"].length;
        yield return new WaitForSeconds(time);
        
        // Eject!
        while(unsmeltables.Count != 0)
        {
            Transform proj = unsmeltables[Random.Range(0, unsmeltables.Count)].transform;
            proj.LookAt(transform.FindChild("ShootLoc"));
            float force = 500f * proj.GetComponent<Rigidbody>().mass;
            proj.GetComponent<Rigidbody>().AddForce(proj.forward * force);

            yield return new WaitForSeconds(1f);
        }

        door.ToggleDoor();
        //unsmeltables.Clear();
        //ejecting = false;
    }
}
