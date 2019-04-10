using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleVAS;

namespace SimpleVAS
{
    public class ForcedChoiceManager : MonoBehaviour
    {

        List<string> questionList = new List<string>();

        public Text questionUI;
        public Button nextButton;
        public Toggle toggleYes;
        public ToggleGroup toggleGroup;

        public CsvWrite csvWriter;

        private int currentItem;

        // Use this for initialization
        void Start() {

            currentItem = 0;
            questionList = CsvRead.questionnaireInput;
            questionUI.text = questionList[currentItem];
            nextButton.interactable = false;
            LabelChanger.instance.NextLabel(currentItem);

        }

        public void OnToggleSelection() {
            nextButton.interactable = true;
        }


        public void OnNextButton() {

            if (toggleYes.isOn)
                QuestionManager.ResponseValue = "1";
            else
                QuestionManager.ResponseValue = "0";

            

            QuestionManager.questionnaireItem = currentItem.ToString();

            csvWriter.onNextButtonPressed();

            toggleGroup.SetAllTogglesOff();
            nextButton.interactable = false;

            currentItem++;
            LabelChanger.instance.NextLabel(currentItem);

            if (currentItem < questionList.Count)
                questionUI.text = questionList[currentItem];


            else if (currentItem == questionList.Count)
            {
                currentItem = 0;
                questionList.Clear();

                SceneManager.LoadScene("VAS");
            }
        }
    }
}
