using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour {

    // Global config
    public bool canBreak;
    public List<string> acceptedTools;
    public bool canPickedUp;
    
    // Click managers
    public virtual void LClick(GameObject source)
    {
        Debug.Log("Wrong script got called :?");
    }

    public virtual void RClick(GameObject source)
    {
        Debug.Log("Wrong script got called :?");

    }

    // Helpful functions
    public bool HasRequiredTool(GameObject player, string specificAcceptedTool = default(string))
    {
        // Only do this if the click is from a player (and has correct components)
        if (player.name.Contains("Player"))
        {
            // We should look at the LHand instead of the player itself
            player = player.transform.FindChild("LHand").gameObject;

            if (!player.transform.FindChild("Tool"))
                return false;

            // Check if an accepted tool is used
            // ..that actually exists...
            if (specificAcceptedTool != null && player.transform.FindChild("Tool"))
            {
                if (specificAcceptedTool == player.transform.FindChild("Tool").GetComponent<Tool>().toolType)
                    // Everything is good, procceed.
                    return true;
            }
            else if (acceptedTools.Contains(player.transform.FindChild("Tool").GetComponent<Tool>().toolType))
                // Everything is good, procceed.
                return true;
        }

        // If everything fails...
        return false;
    }
}
