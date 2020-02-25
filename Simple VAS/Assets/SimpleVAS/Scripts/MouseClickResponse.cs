using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityPsychBasics {
    public class MouseClickResponse : MonoBehaviour {

        private TaskManager _taskManager;
        public bool useMouseClickSelector;

        [HideInInspector]
        public bool orderLeft1Right2;

        private float[] mouseResponse;// = new int[] {1, 2};


        private void Start()
        {
            _taskManager = TaskManager.instance;

            if (useMouseClickSelector)
                _taskManager.setValueOutside = true;

            orderLeft1Right2 = BasicDataConfigurations.mouseClickOrder;

            if (orderLeft1Right2)
                mouseResponse = new float[] { 1, 2 };
            else
                mouseResponse = new float[] { 2, 1 };

        }

        void Update () {

            if(useMouseClickSelector){
		        if(Input.GetMouseButtonDown(0)){
                    _taskManager.OutsideResponseValue(mouseResponse[0]);
                    _taskManager.OnNextButton();
                }

                if(Input.GetMouseButtonDown(1)) {
                    _taskManager.OutsideResponseValue(mouseResponse[1]);
                    _taskManager.OnNextButton();
                }

            }
	    }
	

    }
}
