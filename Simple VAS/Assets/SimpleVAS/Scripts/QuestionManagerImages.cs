using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SimpleVAS
{
    public class QuestionManagerImages : MonoBehaviour
    {

        List<Sprite> questionList = new List<Sprite>();

        public Image _image;
        public Button nextButton;

        public ToggleGroup toggleGroup;

        public CsvWrite csvWriter;

        private int currentItem;

        // Use this for initialization
        void Start()
        {

            currentItem = 0;
            questionList = ImageRead.imageSprites;
            _image.sprite = questionList[currentItem];
            nextButton.interactable = false;

        }

        public void OnToggleSelection()
        {
            nextButton.interactable = true;
        }


        public void OnNextButton()
        {

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

            currentItem++;

            if (currentItem < questionList.Count)
                _image.sprite = questionList[currentItem];


            else if (currentItem == questionList.Count)
            {
                currentItem = 0;
                questionList.Clear();

                SceneManager.LoadScene("VAS");
            }
        }
    }
}
