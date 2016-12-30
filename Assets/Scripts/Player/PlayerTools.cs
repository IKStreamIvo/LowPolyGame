using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTools : MonoBehaviour {

    // DEBUG REMOVE LATER
    public GameObject axe;
    public GameObject pickaxe;
    public GameObject antiGravTool;
    public GameObject empty;

    // Config
    public bool canTool;
    public Vector3 toolOffset;

    // Runtime stuff
    public bool hasTool;
    private bool allowedToolSwitch;


	// Use this for initialization
	void Start () {
        EquipTool(empty);
	}
	
	// Update is called once per frame
	void Update () {
        //DEBUG
            if (Input.GetKeyDown(KeyCode.Keypad0))
                EquipTool(empty);

            else if (Input.GetKeyDown(KeyCode.Keypad1))
                EquipTool(axe);

           else if (Input.GetKeyDown(KeyCode.Keypad2))
                EquipTool(pickaxe);

            else if (Input.GetKeyDown(KeyCode.Keypad3))
                EquipTool(antiGravTool);

    }

    // Specific functions
    public void EquipTool(GameObject tool)
    {
        Debug.Log("Equip: " + tool.name);

        allowedToolSwitch = false;

        if (hasTool)
        {
            // First unequip our current tool
            UnequipTool();
        }

        GameObject newTool = Instantiate(tool, transform.position + tool.transform.position, new Quaternion(0,0,0,0), transform);
        newTool.name = "Tool";

        hasTool = true;
    }

    public void UnequipTool()
    {
        Debug.Log("HasTool: " + hasTool);
        
        if (hasTool)
        {
            Destroy(transform.FindChild("Tool").gameObject);
            hasTool = false;
        }
    }
}
