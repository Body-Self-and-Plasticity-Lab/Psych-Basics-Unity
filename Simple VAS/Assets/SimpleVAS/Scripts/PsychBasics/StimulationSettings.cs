using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPsychBasics
    {
        public class StimulationSettings : MonoBehaviour {

        public static int counter;
        public string sceneAfterFirst, sceneAfterSecond;
        public List<string> conditions = new List<string>(); 

	    // Use this for initialization
	    void Start () {
            //example for repeating stimulation scene but with different tasks after each repetition.
            counter++;
            Debug.Log("current repetition " + counter);
            if(counter ==1)
                LoadScene.instance.sceneToLoad = sceneAfterFirst;
            if (counter == 2){
                LoadScene.instance.sceneToLoad = sceneAfterSecond;
                counter = 0;
            }

            //conditionally check which is the current condition to activate the settings for it, e.g. synch/asynch, example:
            if (ConditionDictionary.selectedOrder[CsvWrite.instance.condition] == conditions[CsvWrite.instance.condition]) {
                //DO
                Debug.Log("activate the settings for " + conditions[CsvWrite.instance.condition]);
            }

            Debug.Log("settings for condition " + ConditionDictionary.selectedOrder[CsvWrite.instance.condition]);           
            

        }
	
	    // Update is called once per frame
	    void Update () {
		
	    }
    }
}
