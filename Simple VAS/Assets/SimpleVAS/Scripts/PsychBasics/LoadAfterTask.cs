using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityPsychBasics
{
        public class LoadAfterTask : MonoBehaviour {

        public string sceneBeforeLastCondition, sceneAfterLastCondition;

        public static LoadAfterTask instance;

        [HideInInspector]
        public int currentTask;

        public bool withinScene;


        [Tooltip("use only when on withinScene mode")]
        public List<bool> shuffle = new List<bool>();
        [Tooltip("use only when on withinScene mode")]
        public List<bool> useImage = new List<bool>();
        [Tooltip("use only when on withinScene mode")]
        public List<bool> analogueScale = new List<bool>();
        [Tooltip("use only when on withinScene mode")]
        public List<bool> useMouseClickSelector = new List<bool>();

        private TaskManager _taskManager;
        private MouseClickResponse _mouseClickResponse;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            _taskManager = TaskManager.instance;   
        }

        private void Start() {
            _mouseClickResponse = MouseClickResponse.instance;

            if (withinScene)
                SetWithinConditions(false);
            else
                _taskManager.InitializeValuesListsAndObjects();
        }


        public void LoadBeforeLast() {
            if (!withinScene)
                SceneManager.LoadScene(sceneBeforeLastCondition);
            else 
                SetWithinConditions(false);                         
        }

        public void LoadAfterLast() {
            if (!withinScene)
                SceneManager.LoadScene(sceneAfterLastCondition);
            else
                SetWithinConditions(true);
        }

        private void SetWithinConditions(bool isLast) {


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
