using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnityPsychBasics
{
    public class ScaleManager : MonoBehaviour 
    {

        public GameObject togglePrefab;
        public ToggleGroup toggleGroup;
        public GameObject scrollbar;

        [HideInInspector] public string minVASLabel, midVASLabel, maxVASLabel;
        [HideInInspector] public List<string> likertItems = new List<string>();

        public static ScaleManager instance;

        private void Awake()
        {
            if (instance == null) instance = this;
        }

        public void CreateToggles()
        {
            foreach(Transform child in toggleGroup.GetComponent<Transform>())
                Destroy(child.gameObject);

            StartCoroutine(ToggleCreationCoroutine());

        }

        private IEnumerator ToggleCreationCoroutine(){ //dirty solving bug not calling likert answers and vas labels
            yield return new WaitForSeconds(0.1f);//delays for a few moments artificially since it depends on variables called at the same time (on Start).

            if (!TaskManager.instance.useAnalogueScale)
                for (int i = 0; i < likertItems.Count; i++)
                {
                    GameObject _instanciatedPrefab = Instantiate(togglePrefab, Vector3.zero, Quaternion.identity);

                    _instanciatedPrefab.transform.SetParent(toggleGroup.gameObject.transform, false);

                    Toggle _toggle = _instanciatedPrefab.GetComponent<Toggle>();
                    _toggle.GetComponentInChildren<Text>().text = likertItems[i];
                    _toggle.group = toggleGroup;

                    _toggle.onValueChanged.AddListener(delegate { TaskManager.instance.OnResponseSelection(); });
             }

            else
                SetAnalogueScaleNames();

        }


        private void SetAnalogueScaleNames(){

            foreach (Transform child in scrollbar.transform) {
                //not very clean solution but works...
                if(SceneManager.GetActiveScene().name == "ImageVAS" || SceneManager.GetActiveScene().name == "ImageVAS_2") {//conditionally flip the scale dimension for the image tasks

                    if (!BasicDataConfigurations.flipScale){
                        if (child.name == "Left label") child.GetComponent<Text>().text = minVASLabel;
                        else if (child.name == "Middle label") child.GetComponent<Text>().text = midVASLabel;
                        else if (child.name == "Right label") child.GetComponent<Text>().text = maxVASLabel;
                    }

                    else {
                        if (child.name == "Right label") child.GetComponent<Text>().text = minVASLabel;
                        else if (child.name == "Middle label") child.GetComponent<Text>().text = midVASLabel;
                        else if (child.name == "Left label") child.GetComponent<Text>().text = maxVASLabel;
                    }
                }   

                else {
                    if (child.name == "Left label") child.GetComponent<Text>().text = minVASLabel;
                    else if (child.name == "Middle label") child.GetComponent<Text>().text = midVASLabel;
                    else if (child.name == "Right label") child.GetComponent<Text>().text = maxVASLabel; 
                }
        }

    }

}
}
