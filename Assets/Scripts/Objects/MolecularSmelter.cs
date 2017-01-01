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
            Smelt();
        }
    }

    // Smelting
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

    void Smelt()
    {

    }
}
