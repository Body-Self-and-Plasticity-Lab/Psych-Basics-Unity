using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SimpleVAS
{
    public class QuestionManagerImages : MonoBehaviour
    {

        private List<Sprite> questionList = new List<Sprite>();
        private List<int> indexList = new List<int>();

        public Image _image;
        public Button nextButton;

        public ToggleGroup toggleGroup;

        public CsvWrite csvWriter;
        public bool shuffle;

        private int currentItem;

        void Start()
        {
            questionList = ImageRead.imageSprites;

            if (!shuffle)
                currentItem = 0;

            else {
                for (int i = 0; i < questionList.Count; i++)
                    indexList.Add(i);

                currentItem = ShuffleValue();
            }

            _image.sprite = questionList[currentItem];
            nextButton.interactable = false;           
        }

        private int ShuffleValue()
        {
            int randomIndex = Random.Range(0, indexList.Count);
            int selectedItem = indexList[randomIndex];
            indexList.RemoveAt(randomIndex);

            return selectedItem;
        }

        public void OnToggleSelection()
        {
            nextButton.interactable = true;
        }


        public void OnNextButton()
        {
            Toggle[] numberOfToggles = toggleGroup.GetComponentsInChildren<Toggle>();

            for (int i = 0; i < numberOfToggles.Length; i++)
                if (numberOfToggles[i].isOn)
                    QuestionManager.ResponseValue = i.ToString();

            nextButton.interactable = false;
            QuestionManager.questionnaireItem = currentItem.ToString();
            csvWriter.onNextButtonPressed();

            toggleGroup.SetAllTogglesOff();              

            if (!shuffle) {

                currentItem++;

                if (currentItem < questionList.Count)
                    _image.sprite = questionList[currentItem];

                else if (currentItem == questionList.Count)
                    QuestionsExhausted();
            }

            else {

                if (indexList.Count != 0) {
                    currentItem = ShuffleValue();
                    _image.sprite = questionList[currentItem];    
                }

                else if (indexList.Count == 0)
                    QuestionsExhausted();
            }
        }

        void QuestionsExhausted()
        {
            currentItem = 0;
            questionList.Clear();

            SceneManager.LoadScene("VAS");
        }
    }


}
