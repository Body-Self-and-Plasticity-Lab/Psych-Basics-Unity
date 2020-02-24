using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityPsychBasics {
    public class MouseClickResponse : MonoBehaviour {

        private TaskManager _taskManager;
        public bool useMouseClickSelector;
        public bool orderLeft1Right2;

        private int[] mouseResponse;// = new int[] {1, 2};


        private void Start()
        {
            _taskManager = TaskManager.instance;
            _taskManager.setValueOutside = true;

            if (orderLeft1Right2)
                mouseResponse = new int[] { 1, 2 };
            else
                mouseResponse = new int[] { 2, 1 };
        }

        void Update () {

            if(useMouseClickSelector){
		        if(Input.GetMouseButtonDown(0)){
                    _taskManager.ResponseValue(mouseResponse[0]);
                    _taskManager.OnNextButton();
                }

                if(Input.GetMouseButtonDown(1)) {
                    _taskManager.ResponseValue(mouseResponse[1]);
                    _taskManager.OnNextButton();
                }

            }
	    }
	

    }
}
