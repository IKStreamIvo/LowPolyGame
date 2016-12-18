using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTools : MonoBehaviour {

    // DEBUG REMOVE LATER
    public GameObject axe;
    public GameObject pickaxe;
    public GameObject antiGravTool;

    // Config
    public bool canTool;
    public Vector3 toolOffset;

    // Runtime stuff
    public bool hasTool;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //DEBUG
        // Axe
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            if (hasTool)
            {
                // Unequip
                UnequipTool();
            }
            else
            {
                // Equip
                EquipTool(axe);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (hasTool)
            {
                // Unequip
                UnequipTool();
            }
            else
            {
                // Equip
                EquipTool(pickaxe);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            if (hasTool)
            {
                // Unequip
                UnequipTool();
            }
            else
            {
                // Equip
                EquipTool(antiGravTool);
            }
        }
    }

    // Specific functions
    public void EquipTool(GameObject tool)
    {
        // First unequip our current tool
        UnequipTool();
        
        GameObject newTool = Instantiate(tool, transform.position + tool.transform.position, new Quaternion(0,0,0,0), transform);
        newTool.name = "Tool";

        hasTool = true;
    }

    public void UnequipTool()
    {
        if (hasTool)
        {
            Destroy(transform.FindChild("Tool").gameObject);
            hasTool = false;
        }
    }
}
