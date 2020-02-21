using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnityPsychBasics {
    public class VASManager : MonoBehaviour {

        public Text questionUI;
        public Button nextButton;
        public Scrollbar scrollValue;

        private CsvWrite _csvWriter;
        private CsvRead _csvReader;

        public static string questionnaireItem, ResponseValue;

        public bool shuffle;

        private List<string> questionList = new List<string>();
        private List<int> indexList = new List<int>();
        private int currentItem;


        public static int currentCondition;

        private void Awake() {
            _csvWriter = FindObjectOfType<CsvWrite>();
            _csvReader = FindObjectOfType<CsvRead>();
        }

        // Use this for initialization
        void Start()
        {
            for (int i = 0; i <  _csvReader.questionnaireInput.Count; i++)
                questionList.Add(_csvReader.questionnaireInput[i]);

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

            _csvWriter.response = scrollValue.value.ToString();
            _csvWriter.LogTrial();
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
