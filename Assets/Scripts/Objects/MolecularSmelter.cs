using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolecularSmelter : WorldObject {

    private SmelterDoor door;

    void Start ()
    {
        door = transform.FindChild("Door").GetComponent<SmelterDoor>();
    }


    private List<GameObject> smeltables;
    private List<GameObject> unsmeltables;

    void OnTriggerEnter(Collider obj)
    {
        smeltables.Add(obj.gameObject);
    }

    void OnTriggerExit(Collider obj)
    {
        smeltables.Remove(obj.gameObject);
    }


    public override void RClick(GameObject source)
    {
        if (door.open)
        {
            // Play blocked-sound?
            return;
        }
        else
        {

        }
    }

    void Smelt()
    {

    }

}
