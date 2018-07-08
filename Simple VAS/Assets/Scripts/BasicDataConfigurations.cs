using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

namespace SimpleVAS
{
public class BasicDataConfigurations : MonoBehaviour {

	public InputField nameField, ageField;
	public Text genderField, handednessField;
	public Button nextButton;
	public static string ID, age, gender, handedness, conditionOrder;

	// Use this for initialization
	void Start () {
		nextButton.interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (ID != null && age != null)
			nextButton.interactable = true;
	}

	public void userName() {
		ID = nameField.text;
	}

	public void userAge() {
		age = ageField.text;
	}

	public void OnNextButton () {

		gender = genderField.text;
		handedness = handednessField.text;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

	}

}

}