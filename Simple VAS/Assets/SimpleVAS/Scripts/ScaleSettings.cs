using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityPsychBasics
{
    public class ScaleSettings : MonoBehaviour {

        public GameObject togglePrefab;
        public Transform toggleParent;

        public string scaleMin, scaleMid, scaleMax;

        public Text minText, midText, maxText;

        public int likertSize;

        private TaskManager _taskManager;

        private void Start() {

            _taskManager = TaskManager.instance;

            for (int i = 1; i <= likertSize; i++)
                CreateToggle();
        }

        private void CreateToggle() {

            GameObject newToggle = Instantiate(togglePrefab, new Vector3(0, 0, 0), Quaternion.identity);

            newToggle.transform.SetParent(toggleParent, false);

            //newToggle = toggleGroup;

            Toggle toggleBehavior = newToggle.GetComponent<Toggle>();

            toggleBehavior.onValueChanged.AddListener(delegate { ToggleAction(toggleBehavior); });

        }
        
        private void ToggleAction(Toggle changed){
            Debug.Log("toggle pressed");
            _taskManager.OnResponseSelection();
        }
    }
}
