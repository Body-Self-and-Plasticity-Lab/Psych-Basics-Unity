using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnityPsychBasics
{
    public class VASImageManager : MonoBehaviour
    {

        private List<Sprite> questionList = new List<Sprite>();
        private List<int> indexList = new List<int>();

        public Image _image;
        public Button nextButton;
        public Scrollbar scrollValue;

        private CsvWrite _csvWriter;
        public bool shuffle;

        private int currentItem;

        private void Awake()
        {
            _csvWriter = FindObjectOfType<CsvWrite>();
        }

        void Start()
        {
            questionList = ImageRead.imageSprites;

            if (!shuffle)
                currentItem = 0;

            else
            {
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
            _csvWriter.response = scrollValue.value.ToString();

            nextButton.interactable = false;
            VASManager.questionnaireItem = currentItem.ToString();
            _csvWriter.LogTrial();
            scrollValue.value = 0.5f;



            if (!shuffle)
            {

                currentItem++;

                if (currentItem < questionList.Count)
                    _image.sprite = questionList[currentItem];

                else if (currentItem == questionList.Count)
                    QuestionsExhausted();
            }

            else
            {

                if (indexList.Count != 0)
                {
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
