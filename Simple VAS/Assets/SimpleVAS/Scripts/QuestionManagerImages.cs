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

        // Use this for initialization
        void Start()
        {
            questionList = ImageRead.imageSprites;

            for (int i = 0; i < questionList.Count; i++) { 
                indexList.Add(i);
                Debug.Log("added " + i);
            }
            if (!shuffle)
                currentItem = 0;
            else
                currentItem = Shuffle();

            Debug.Log(currentItem);

            
            _image.sprite = questionList[currentItem];
            nextButton.interactable = false;

        }

        private int Shuffle()
        {
            int randomIndex = Random.Range(0, indexList.Count);
            int selectedItem = indexList[randomIndex];
            indexList.RemoveAt(randomIndex);

            return randomIndex;
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

            if (shuffle)
                currentItem++;
            else
                currentItem = Shuffle();

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
