using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	public string sceneToLoad;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnNextButton () {

		if (sceneToLoad != "")
			SceneManager.LoadScene (sceneToLoad);
		else
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	} 
}
