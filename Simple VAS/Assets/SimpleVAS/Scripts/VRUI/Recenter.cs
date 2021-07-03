using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Recenter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InputTracking.disablePositionalTracking = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("c"))
        {
            InputTracking.Recenter();
        }
	}
}
