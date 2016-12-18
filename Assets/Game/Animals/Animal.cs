using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour {

    // Required if you want it to be ridable
    // else you can leave it empty.
    public GameObject saddlePrefab;
    public Vector3 rideOffset;


    private bool hasSaddle;
    private bool mounted;


    // Click managers
    public void LClick(GameObject source)
    {
        Saddle(true);
    }

    public void RClick(GameObject source)
    {
        Mount(source, true);
    }


    // Standard functions
    void Start()
    {
        // animSpeed | walkSpeed
        //         1 | 0.3
        //         2 | 0.6
        //         3 | 0.9
        GetComponent<Animation>()["Armature|WalkingCycle"].speed = 2f;
    }


    public float speed = 6.0F;
    private Vector3 moveDirection = Vector3.zero;
    public float turnSpeed = 5.0f;

    void Update()
    {
        if (mounted)
        {
            CharacterController controller = GetComponent<CharacterController>();

            // Generate movement
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection *= speed;

            // Rotation
            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * turnSpeed);

            // Execute
            controller.Move(moveDirection * Time.deltaTime);
        }
    }


    // Specific functions
    public void Saddle(bool doSaddle)
    {
        if (doSaddle)
        {
            if (hasSaddle == false && saddlePrefab)
            {
                GameObject saddle = (GameObject)Instantiate(saddlePrefab, transform.position + saddlePrefab.transform.position, transform.rotation);
                saddle.transform.parent = transform;
                saddle.name = "Saddle";

                hasSaddle = true;
            }
        }
        else
        {
            if (hasSaddle == true)
            {
                Destroy(transform.FindChild("Saddle").gameObject);

                hasSaddle = false;
            }
        }
    }

    public void Mount(GameObject player, bool mount)
    {
        if (hasSaddle)
        {
            if (mount && !mounted) // Mount
            {
                // Setup stuff
                mounted = true;
                player.transform.parent = transform.FindChild("Saddle");

                // Disable player movement;
                player.GetComponent<PlayerMovement>().riding = true;

                // Move the player
                player.transform.position = transform.FindChild("Saddle").position + rideOffset;
                player.transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
                player.transform.FindChild("Camera").rotation = new Quaternion(0, 0, 0, 0);
            }
            else if(!mount && mounted) // Dismount
            {

            }
        }
    }
}
