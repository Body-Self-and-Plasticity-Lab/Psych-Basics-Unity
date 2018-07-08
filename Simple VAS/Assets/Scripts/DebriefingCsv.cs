using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleVAS 
{
public class DebriefingCsv : MonoBehaviour {


	public Toggle hallucinations;
	public Text comments;

	// Use this for initialization
	void Start () {
		WriteToFile ("subject ID", "hallucinations", "comments");
	}


	public void onNextButtonPressed(){

		int hallucinationsValue;

		if (hallucinations.isOn) hallucinationsValue = 1;
		else hallucinationsValue = 0;

		WriteToFile (BasicDataConfigurations.ID, hallucinationsValue.ToString(), comments.text);
	}

	void WriteToFile(string a, string b, string c){

		string stringLine =  a + "\t" + b + "\t" + c;

		System.IO.StreamWriter file = new System.IO.StreamWriter("./Logs/" + BasicDataConfigurations.ID + "_debriefing.csv", true);
		file.WriteLine(stringLine);
		file.Close();	
	}
}

}