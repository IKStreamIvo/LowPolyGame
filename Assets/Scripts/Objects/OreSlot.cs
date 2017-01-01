using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreSlot : MonoBehaviour {

    // Slot config (dont touch manually)
    public GameObject orePrefab;
    public string oreName;

    // Runtime
    public GameObject ore;
    public bool hasOre;

    public IEnumerator Replenish(float time)
    {
        yield return new WaitForSeconds(time);

        if (!ore)
        {
            ore = Instantiate(orePrefab, transform.position, Quaternion.identity, transform);
            ore.name = oreName;

            transform.parent.parent.GetComponent<OreRock>().possibleSlots.Add(gameObject);
        }
    }

    public void Mine()
    {
        if (ore)
        {
            // Make sure this ore cant be mined again
            transform.parent.parent.GetComponent<OreRock>().possibleSlots.Remove(gameObject);

            // Free the ore!
            ore.GetComponent<WorldObject>().canPickedUp = true;
            // Turn the mesh of the rock off
            StartCoroutine(ColliderWait(.25f));

            ore.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            Vector3 dir = (ore.GetComponent<Crystal>().collisionPoint - ore.transform.position) * 50f;
            dir = dir.normalized;
            dir = dir + transform.up;
            ore.GetComponent<Rigidbody>().AddForce(dir * 100f);

            ore.transform.parent = null;
            ore = null;

            

            // Start replenish timer
            StartCoroutine(Replenish(10));
        }
    }

    IEnumerator ColliderWait(float time)
    {
        transform.parent.parent.FindChild("Rock").GetComponent<MeshCollider>().enabled = false;

        yield return new WaitForSeconds(time);

        transform.parent.parent.FindChild("Rock").GetComponent<MeshCollider>().enabled = true;
    }
}
