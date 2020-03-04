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

        private void Awake()
        {
            if (instance == null)
                instance = this;

            _taskManager = TaskManager.instance;
        }

        private void Start()
        {
            if (withinScene)
            {
                SetWithinConditions();
            }
        }


        public void LoadBeforeLast() {
            if (!withinScene)
                SceneManager.LoadScene(sceneBeforeLastCondition);

            else {
                SetWithinConditions();
                
            }
        }

        private void SetWithinConditions() {

            if(currentTask < useImage.Count){
                _taskManager.useImages = useImage[currentTask];
                _taskManager.useAnalogueScale = analogueScale[currentTask];
                _taskManager.shuffle = shuffle[currentTask];
                currentTask++;
                Debug.Log("current task is " + currentTask);
                _taskManager.InitializeValuesListsAndObjects();
            }
            else
                SceneManager.LoadScene(sceneBeforeLastCondition);
        }


        public void LoadAfterLast(){
                SceneManager.LoadScene(sceneAfterLastCondition);
        }

                
    }
}
