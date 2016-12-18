using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : ClickableObject {

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
                ModifyHealth(5f);
            else if (brokenTree && canBreak)
                RemoveLeaves();
        }
    }

    public override void RClick(GameObject source)
    {
        Debug.Log("1");
    }


    // Config
    public float health = 10f;


    // Health Managers
    // Use negative damage to add more health
    public void ModifyHealth(float damage)
    {
        if (health - damage > 0f)
            health = health - damage;
        else
        {
            health = health - damage;
            Break();
        }
    }

    // Specific functions
    public float fallForce = 60f;
    public void Break()
    {
        //fallForce = fallForce * transform.lossyScale.y;

        // Spawn the two parts of the tree
        GameObject stump = (GameObject)Instantiate(stumpPrefab, transform.position, Quaternion.identity);
        GameObject broken = (GameObject)Instantiate(brokenTreePrefab, stump.transform.position + new Vector3(brokenTreePrefab.transform.position.x, brokenTreePrefab.transform.position.y * transform.lossyScale.y, brokenTreePrefab.transform.position.z), Quaternion.identity);

        // Make the tree part fall
        broken.GetComponent<Rigidbody>().AddForce(transform.forward * fallForce);

        // Delete the old tree
        Destroy(gameObject);
    }

    public void RemoveLeaves()
    {
        GameObject leaves = transform.FindChild("Leaves").gameObject;
        Destroy(leaves);
    }
}
