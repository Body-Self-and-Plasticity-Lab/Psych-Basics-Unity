using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityPsychBasics
{
        public class TaskSettings : MonoBehaviour {

        public string sceneBeforeLastCondition, sceneAfterLastCondition;

        public static TaskSettings instance;

        public int currentTask;

        public bool withinScene;

        public bool shuffleBool, useImageBool, useAnalogueScaleBool, useMouseBool;

        public int numberOfConditions;

        public List<bool> shuffle = new List<bool>();
        public List<bool> useImage = new List<bool>();
        public List<bool> analogueScale = new List<bool>();
        public List<bool> useMouseClickSelector = new List<bool>();

        private TaskManager _taskManager;
        private MouseClickResponse _mouseClickResponse;
        
        private void Awake() {
            if (instance == null)
                instance = this;

            _taskManager = TaskManager.instance;   
        }

        private void Start() {
            _mouseClickResponse = MouseClickResponse.instance;

            if (withinScene)
                SetWithinScene(false);
            else
                SetForSeparateScenes();
        }


        public void LoadBeforeLast() {
            if (!withinScene)
                SceneManager.LoadScene(sceneBeforeLastCondition);
            else 
                SetWithinScene(false);                         
        }

        public void LoadAfterLast() {
            if (!withinScene)
                SceneManager.LoadScene(sceneAfterLastCondition);
            else
                SetWithinScene(true);
        }

        private void SetForSeparateScenes(){
            _mouseClickResponse.ActivateSelector(shuffleBool);
            _taskManager.useImages = useImageBool;
            _taskManager.useAnalogueScale = useAnalogueScaleBool;
            _taskManager.shuffle = shuffleBool;

            _taskManager.InitializeValuesListsAndObjects();
        }

        private void SetWithinScene(bool isLast) {


            if (currentTask < useImage.Count) {

                _mouseClickResponse.ActivateSelector(useMouseClickSelector[currentTask]);
                _taskManager.useImages = useImage[currentTask];
                _taskManager.useAnalogueScale = analogueScale[currentTask];
                _taskManager.shuffle = shuffle[currentTask];
                
                _taskManager.InitializeValuesListsAndObjects();

                currentTask++;
            }

            else {
                if(!isLast)
                    SceneManager.LoadScene(sceneBeforeLastCondition);
                else
                    SceneManager.LoadScene(sceneAfterLastCondition);
            }

        }

                
    }
}
