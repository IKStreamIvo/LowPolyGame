using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAroundObject : MonoBehaviour {

    public Transform target;
    public float speed;

    void Update()
    {
        transform.LookAt(target);
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
