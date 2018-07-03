using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CsvWrite : MonoBehaviour {


	private string condition;

	// Use this for initialization
	void Start () {
		WriteToFile ("subject ID", "age", "gender", "handedness", "question ID", "condition", "value");
	}

	// Update is called once per frame
	void Update () {

	}

	public void onNextButtonPressed(){
		WriteToFile (BasicDataConfigurations.ID, BasicDataConfigurations.age, BasicDataConfigurations.gender, BasicDataConfigurations.handedness, QuestionManager.questionnaireItem,  BasicDataConfigurations.conditionOrder, QuestionManager.VASvalue);

	}

	public void onBasicDataEntered(){
		
			
	}


	void WriteToFile(string a, string b, string c, string d, string e, string f, string g){

		string stringLine =  a + "," + b + "," + c + "," + d + "," + e + "," + f + "," + g;

		System.IO.StreamWriter file = new System.IO.StreamWriter("./Logs/" + BasicDataConfigurations.ID + "_log.csv", true);
		file.WriteLine(stringLine);
		file.Close();	
	}
}
