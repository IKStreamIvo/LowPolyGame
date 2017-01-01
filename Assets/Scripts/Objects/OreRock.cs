using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreRock : WorldObject {
    
    // Config
    public GameObject[] oreParts;
    public float standardReplenishTime = 10f;

    // Slot stuff
    public GameObject slotPrefab;
    public string oreName;

    // Runtime
    public List<GameObject> possibleSlots;

    
    void Start()
    {
        // Create a slot for every orePart
        foreach(GameObject part in oreParts)
        {
            GameObject slot = Instantiate(slotPrefab, transform.position, Quaternion.identity, transform.FindChild("Ore"));
            slot.GetComponent<OreSlot>().orePrefab = part;
            slot.GetComponent<OreSlot>().oreName = oreName;
            StartCoroutine(slot.GetComponent<OreSlot>().Replenish(0));
        }
        
    }

    public override void LClick(GameObject source)
    {
        if (HasRequiredTool(source))
        {
            if(possibleSlots.Count > 0)
                possibleSlots[Random.Range(0, possibleSlots.Count)].GetComponent<OreSlot>().Mine();
        }
    }

    public override void RClick(GameObject source)
    {
        
    }

}
