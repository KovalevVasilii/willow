using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

    public Transform target;
    int distance = -10;
    float lift = 1.5f;
	
	void Update () {
        transform.position = new Vector3(0, lift, distance) + target.position;
        transform.LookAt(target);
	}
}
