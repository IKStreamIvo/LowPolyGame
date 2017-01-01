using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : WorldObject {

    // Config
    public bool normalTree;
    public bool brokenTree;

    // Prefabs
    public GameObject stumpPrefab;
    public GameObject brokenTreePrefab;


    // Click Managers
	public override void LClick(GameObject source)
    {
        // Check if holds the required tool.
        if (HasRequiredTool(source))
        {
            if (normalTree && canBreak)
                ModifyHealth(5f, source);
        }
    }

    public override void RClick(GameObject source)
    {
        // Check if holds the required tool.
        if (HasRequiredTool(source))
        {
            if (brokenTree && canBreak)
                RemoveLeaves();
        }
    }


    // Config
    public float health = 10f;


    // Health Managers
    // Use negative damage to add more health
    public void ModifyHealth(float damage, GameObject source)
    {
        if (health - damage > 0f)
            health = health - damage;
        else
        {
            health = health - damage;
            Break(source);
        }
    }

    // Specific functions
    public float fallForce = 60f;

    public void Break(GameObject source)
    {
        fallForce = fallForce * transform.localScale.y;
        
        // Spawn the two parts of the tree
        GameObject stump = (GameObject)Instantiate(stumpPrefab, transform.position, Quaternion.identity);
        GameObject broken = (GameObject)Instantiate(brokenTreePrefab, stump.transform.position + new Vector3(brokenTreePrefab.transform.position.x, brokenTreePrefab.transform.position.y * transform.lossyScale.y, brokenTreePrefab.transform.position.z), Quaternion.identity);

        stump.transform.localScale = new Vector3(stumpPrefab.transform.localScale.x * transform.lossyScale.x, stumpPrefab.transform.localScale.y * transform.lossyScale.y, stumpPrefab.transform.localScale.z * transform.lossyScale.z);
        broken.transform.localScale = new Vector3(brokenTreePrefab.transform.localScale.x * transform.lossyScale.x, brokenTreePrefab.transform.localScale.y * transform.lossyScale.y, brokenTreePrefab.transform.localScale.z * transform.lossyScale.z);

        // Make the tree part fall
        broken.GetComponent<Rigidbody>().AddForce(source.transform.forward * fallForce);

        // Delete the old tree
        Destroy(gameObject);
    }

    public void RemoveLeaves()
    {
        GameObject leaves = transform.FindChild("Leaves").gameObject;
        Destroy(leaves);
        canPickedUp = true;
    }
}
