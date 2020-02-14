using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleVAS;

namespace SimpleVAS
{
    public class QuestionManager : MonoBehaviour
    {

        List<string> questionList = new List<string>();
        private List<int> indexList = new List<int>();

        public Text questionUI;
        public Button nextButton;
        public Scrollbar scrollValue;

        public CsvWrite csvWriter;

        public static string questionnaireItem, ResponseValue;

        public bool shuffle;

        private int currentItem;


        public static int currentCondition;

        // Use this for initialization
        void Start()
        {

            questionList = CsvRead.questionnaireInput;

            if (!shuffle)
                currentItem = 0;
            else
            {
                for (int i = 0; i < questionList.Count; i++)
                    indexList.Add(i);

                currentItem = ShuffleValue();
            }

            questionUI.text = questionList[currentItem];
            nextButton.interactable = false;
        }

        public void OnScaleSelection()
        {

            nextButton.interactable = true;

        }

        private int ShuffleValue()
        {
            int randomIndex = Random.Range(0, indexList.Count);
            int selectedItem = indexList[randomIndex];
            indexList.RemoveAt(randomIndex);

            return selectedItem;
        }

        public void OnNextButton()
        {

            nextButton.interactable = false;
            questionnaireItem = currentItem.ToString();
            ResponseValue = scrollValue.value.ToString();
            csvWriter.onNextButtonPressed();
            scrollValue.value = 0.5f;

            if (!shuffle)
            {
                currentItem++;

                if (currentItem < questionList.Count)
                    questionUI.text = questionList[currentItem];

                else if (currentItem == questionList.Count)
                    QuestionsExhausted();
            }

            else
            {
                if (indexList.Count != 0)
                {
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
            currentCondition = currentCondition + 1;

            if (currentCondition < ConditionDictionary.selectedOrder.Length) SceneManager.LoadScene("Inter");
            else if (currentCondition == ConditionDictionary.selectedOrder.Length) SceneManager.LoadScene("Debriefing");
        }
    }
}
