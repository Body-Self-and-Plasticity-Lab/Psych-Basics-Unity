using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnityPsychBasics 
{
	public class CsvWrite : MonoBehaviour {

        public static CsvWrite instance;

        [Tooltip("Note that 8 variables are coded in, edit script if this has to change")]
        public List<string> varNames = new List<string>();
        public List<string> varValues = new List<string>();

        [HideInInspector]
        public int item, condition;
        [HideInInspector]
        public float response;

		//This allows the start function to be called only once.
		void Awake()
		{
            if (instance == null)
                instance = this;

			DontDestroyOnLoad(this.gameObject);
		}

		void Start () {
            foreach (var item in varNames) { 
                 varValues.Add(null);
            }

            WriteToFile(varNames);
        }


        public void SetVariables ()
        {
            varValues[0] = BasicDataConfigurations.ID;
            varValues[1] = BasicDataConfigurations.age;
            varValues[2] = BasicDataConfigurations.gender;
            varValues[3] = BasicDataConfigurations.handedness;
            varValues[4] = SceneManager.GetActiveScene().name;
            varValues[5] = ConditionDictionary.selectedOrder[item];
            varValues[6] = item.ToString();
            varValues[7] = response.ToString();
        }

        public void SetSingleVariable(string value, int index) {
            varValues[index] = value;
        }
			

		public void LogTrial(){
            SetVariables();
            if (BasicDataConfigurations.ID == null)
                LoadNull();
            else
                SetVariables();

            WriteToFile(varValues);
        }

		void LoadNull(){
            for (int i = 0; i < varValues.Count; i++) {
                varValues[i] = "na";
            }
		}

		void WriteToFile(List<string> stringList){

            string stringLine = string.Join(",", stringList.ToArray());

			System.IO.StreamWriter file = new System.IO.StreamWriter("./Logs/" + BasicDataConfigurations.ID + "_log.csv", true);
			file.WriteLine(stringLine);
			file.Close();	
		}
	}
}