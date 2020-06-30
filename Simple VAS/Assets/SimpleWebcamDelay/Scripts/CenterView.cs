using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace WebcamDelay {
	public class CenterView : MonoBehaviour {
		
		//Use this script for centering the view on a VR headset, tested only on the Oculus CV1

		void Start () {
			InputTracking.disablePositionalTracking = true;
		}

		void Update () {
			if (Input.GetKeyDown ("c")) UnityEngine.XR.InputTracking.Recenter ();
		}


	}
}
