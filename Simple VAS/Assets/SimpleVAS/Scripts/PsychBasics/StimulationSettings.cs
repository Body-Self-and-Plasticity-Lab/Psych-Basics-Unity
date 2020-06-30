using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebcamDelay;

namespace UnityPsychBasics
    {
        public class StimulationSettings : MonoBehaviour {

        public static int counter;
        public string sceneAfterFirst, sceneAfterSecond;
        public WebcamDisplay _webcamDelay;

        // Use this for initialization
        void Start () {
            //example for repeating stimulation scene but with different tasks after each repetition.

            if (counter < 1) {
                LoadScene.instance.sceneToLoad = sceneAfterFirst;
                counter++;
            }
            else if (counter == 1){
                LoadScene.instance.sceneToLoad = sceneAfterSecond;
                counter = 0;
            }

            //conditionally check which is the current condition to activate the settings for it, e.g. synch/asynch, example:
            if (ConditionDictionary.selectedOrder[CsvWrite.instance.condition] == "sync")
            {
                _webcamDelay.delayTimeSeconds = 0f;
            }
            else
                _webcamDelay.delayTimeSeconds = 1f;

            //Debug.Log("settings for condition " + ConditionDictionary.selectedOrder[CsvWrite.instance.condition]);           
            

        }
	
    }
}
