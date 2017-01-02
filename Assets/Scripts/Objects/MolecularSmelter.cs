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

    public IEnumerator Activate()
    {
        Debug.Log("Smelt!");
        door.locked = true;
        foreach (GameObject ore in smeltables)
        {
            //Calculate progress
            // deel/geheel*100%
            float nr1 = smeltables.IndexOf(ore) + 1;
            float nr2 = smeltables.Count;
            float nr3 = 100;
            float progress = (nr1 / nr2) * nr3;

            List<MeshRenderer> progressBar = new List<MeshRenderer>();
            progressBar.Add(transform.FindChild("ProgressBar").GetChild(0).GetComponent<MeshRenderer>());
            progressBar.Add(transform.FindChild("ProgressBar").GetChild(1).GetComponent<MeshRenderer>());
            progressBar.Add(transform.FindChild("ProgressBar").GetChild(2).GetComponent<MeshRenderer>());
            progressBar.Add(transform.FindChild("ProgressBar").GetChild(3).GetComponent<MeshRenderer>());

            UpdateProgressBar(progress, progressBar);

            GameObject ingot = Instantiate(ore.GetComponent<WorldObject>().smeltedForm, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            Destroy(ore);
            
            yield return new WaitForSeconds(1);

        }
        smeltables.Clear();
        door.locked = false;
    }

    public Material barRed;
    public Material barGreen;
    public void UpdateProgressBar(float progress, List<MeshRenderer> bars) // progress = 0 <-> 100
    {
        // Four parts
        // 25 - 50 - 75 - 100
        Debug.Log("Progress: " + progress);
        if (progress >= 25)
            bars[0].material = barGreen;
        if (progress >= 50)
            bars[1].material = barGreen;
        if (progress >= 75)
            bars[2].material = barGreen;
        if (progress >= 100)
            bars[3].material = barGreen;
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
