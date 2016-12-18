using UnityEngine;
using System.Collections;

public class NavigateToTarget : MonoBehaviour
{

    public Transform[] targets;
    private float testInterval = 1.0f;
    private float elapsedInterval = 0.0f;
    private static System.Random random = new System.Random();
    public bool rotationFix = false;
    public float pauseAtDestinationDuration = 0.0f;
    private bool isPaused = false;
    private float pauseElapsed = 0.0f;
    // Use this for initialization
    void Awake()
    {
        if (targets == null || targets[0] == null)
        {
            this.enabled = false;
        }
    }
    void Start()
    {
        UpdateDestination();

        foreach (Transform t in targets)
            t.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedInterval > testInterval)
        {
            if (pauseAtDestinationDuration == 0)
            {
                UpdateDestination();
            }
            else
            {
                isPaused = true;
            }
            testInterval = random.Next(1);
            elapsedInterval = 0.0f;
        }
        if (!isPaused)
        {
            elapsedInterval += Time.deltaTime;
        }
        else
        {
            pauseElapsed += Time.deltaTime;
            if (pauseElapsed > pauseAtDestinationDuration)
            {
                pauseElapsed = 0.0f;
                isPaused = false;
                UpdateDestination();
            }
        }

    }

    private void UpdateDestination()
    {
        int nextDestination = random.Next(targets.Length - 1);
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (agent.remainingDistance < 1)
        {            
            agent.destination = targets[nextDestination].position;
            if (rotationFix)
            {
               transform.LookAt(targets[nextDestination]);
               transform.Rotate(Vector3.up, 90);
               agent.updateRotation = false;
            }
        }

    }
}
