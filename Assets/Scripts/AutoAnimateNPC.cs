using UnityEngine;
using System.Collections;

/*public class AutoAnimateNPC : MonoBehaviour {

    Vector3 lastPos;
    public AnimationClip walkAnim;
    public AnimationClip idleAnim;
    public bool overrideAnimationSpeeds = false;
    public float idleAnimSpeedModifier = 1.0f;
    public float walkAnimSpeedModifier = 1.0f;

	// Use this for initialization
	void Start () {
        lastPos = transform.position;
        if (overrideAnimationSpeeds)
        {
            animation[walkAnim.name].normalizedSpeed = walkAnimSpeedModifier;
            animation[idleAnim.name].normalizedSpeed = idleAnimSpeedModifier;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<NavMeshAgent>().remainingDistance < 0.5f)
        {
            animation.CrossFade(idleAnim.name, 0.2f);
        }
        else if (lastPos != transform.position)
        {
            animation.Play(walkAnim.name);
        }
        
	}
}
*/