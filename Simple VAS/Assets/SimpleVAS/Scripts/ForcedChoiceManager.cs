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
        private List<int> indexList = new List<int>();

        public Text questionUI;
        public Button nextButton;
        public Toggle toggleYes;
        public ToggleGroup toggleGroup;

        public CsvWrite csvWriter;

        private int currentItem;
        public bool shuffle;

        // Use this for initialization
        void Start() {

            questionList = CsvRead.questionnaireInput;

            if (!shuffle)
                currentItem = 0;

            else {
                for (int i = 0; i < questionList.Count; i++)
                    indexList.Add(i);

                currentItem = ShuffleValue();
            }
           
            questionUI.text = questionList[currentItem];
            nextButton.interactable = false;
        }

        public void OnToggleSelection() {
            nextButton.interactable = true;
        }

        private int ShuffleValue() {
            int randomIndex = Random.Range(0, indexList.Count);
            int selectedItem = indexList[randomIndex];
            indexList.RemoveAt(randomIndex);

            return selectedItem;
        }


        public void OnNextButton() {

            Toggle[] numberOfToggles = toggleGroup.GetComponentsInChildren<Toggle>();

            for (int i = 0; i < numberOfToggles.Length; i++)
            {
                if (numberOfToggles[i].isOn)
                {
                    QuestionManager.ResponseValue = i.ToString();
                }
            }


            nextButton.interactable = false;
            QuestionManager.questionnaireItem = currentItem.ToString();
            csvWriter.onNextButtonPressed();

            toggleGroup.SetAllTogglesOff();       

            if (!shuffle) {
                currentItem++;

                if (currentItem < questionList.Count)
                    questionUI.text = questionList[currentItem];

                else if (currentItem == questionList.Count)
                    QuestionsExhausted();
            }

            else {

                if (indexList.Count != 0) {
                    currentItem = ShuffleValue();
                    questionUI.text = questionList[currentItem];
                }

                else if (indexList.Count == 0)
                    QuestionsExhausted();
            }
        }

        private void QuestionsExhausted()
        {
            currentItem = 0;
            questionList.Clear();

            SceneManager.LoadScene("VAS");
        }
    }
}
