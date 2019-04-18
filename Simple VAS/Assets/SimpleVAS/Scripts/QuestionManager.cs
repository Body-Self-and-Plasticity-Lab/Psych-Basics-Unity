using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleVAS;

namespace SimpleVAS
{
	public class QuestionManager : MonoBehaviour {

		List<string> questionList = new List<string>();

		public Text questionUI;
		public Button nextButton;
		public Scrollbar scrollValue;

		public CsvWrite csvWriter;

		public static string questionnaireItem, ResponseValue;

		private int currentItem;

		public static int currentCondition;

        private void Awake()
        {
            csvWriter = FindObjectOfType<CsvWrite>();
        }
        // Use this for initialization
        void Start () {

           
			currentItem = 0;
			questionList = CsvRead.questionnaireInput;
			questionUI.text = questionList[currentItem];
			nextButton.interactable = false;

		}
			
		public void OnScaleSelection(){
			
			nextButton.interactable = true;

		}


		public void OnNextButton() {
		
			
			questionnaireItem = currentItem.ToString ();
			ResponseValue = scrollValue.value.ToString();
			csvWriter.onNextButtonPressed ();

			currentItem ++;

            scrollValue.value = 0.5f;
            nextButton.interactable = false;

            if (currentItem < questionList.Count) 
				questionUI.text = questionList [currentItem];


			else if (currentItem == questionList.Count) {
                if (SceneManager.GetActiveScene().name == "GenderVAS")
                    SceneManager.LoadScene("ForcedChoice");

                else if (SceneManager.GetActiveScene().name == "VAS") { 
				    currentItem = 0;
				    questionList.Clear();
				    currentCondition = currentCondition + 1;

				    if(currentCondition < ConditionDictionary.selectedOrder.Length) SceneManager.LoadScene("Inter");
					else if (currentCondition == ConditionDictionary.selectedOrder.Length) SceneManager.LoadScene ("Before end");
                }

                else if (SceneManager.GetActiveScene().name == "VAS_end") SceneManager.LoadScene("Goodbye");
            }
		}
	}
}
