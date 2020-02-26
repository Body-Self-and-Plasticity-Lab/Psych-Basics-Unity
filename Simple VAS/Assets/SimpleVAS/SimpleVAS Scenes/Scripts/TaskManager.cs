using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnityPsychBasics {

    public class TaskManager : MonoBehaviour {

        public Text questionUI;
        public Button nextButton;
        public Scrollbar _scrollbar;
        public ToggleGroup _toggleGroup;
        public Image _image;

        public bool shuffle, useImages, useAnalogueScale;
        public float showImageForTime;

        [HideInInspector]
        public bool setValueOutside;
        [HideInInspector]
        public bool allowInput;

        public static TaskManager instance;

        private CsvWrite _csvWriter;
        private CsvRead _csvReader;
        private ImageRead _imageReader;
        private Timer _timer;

        private List<string> questionList = new List<string>();

        private List<Sprite> imageList = new List<Sprite>();
        private List<Sprite> imageList2 = new List<Sprite>();

        private List<int> indexList = new List<int>();

        private int currentItem;
    
        private void Awake() {

            if (instance == null)
                instance = this;

            _csvWriter = CsvWrite.instance;

            if (useImages)
                _imageReader = ImageRead.instance;
            else
                _csvReader = CsvRead.instance;
        }

        void Start() {

            //InitializeValuesListsAndObjects();
            nextButton.interactable = false;

            _timer = Timer.instance;

        }

        public void InitializeValuesListsAndObjects() {

            if(!useImages)
                _csvReader.SetFileToLoad();

            if(setValueOutside) {
                _toggleGroup.gameObject.SetActive(false);
                _scrollbar.gameObject.SetActive(false);
            }

            else {
                if (useAnalogueScale)
                    _toggleGroup.gameObject.SetActive(false);
                else
                    _scrollbar.gameObject.SetActive(false);
            }

            if (useImages) {
                questionUI.gameObject.SetActive(false);

                for (int i = 0; i < _imageReader.imageSprites1.Count; i++)
                    imageList.Add(_imageReader.imageSprites1[i]);

                for (int i = 0; i < _imageReader.imageSprites2.Count; i++)
                    imageList2.Add(_imageReader.imageSprites2[i]);

                if (shuffle)
                    CreateShuffleList();

                StartCoroutine(ShowImageForTime());
                //_image.sprite = imageList[currentItem];
            }

            else {

                _image.gameObject.SetActive(false);

                for (int i = 0; i < _csvReader.questionnaireInput.Count; i++)
                    questionList.Add(_csvReader.questionnaireInput[i]);

                if (shuffle)
                    CreateShuffleList();

                questionUI.text = questionList[currentItem];
                _timer.stopwatch.Start();
            }          
           
        }

        private void CreateShuffleList(){

            if(useImages)
                for (int i = 0; i < imageList.Count; i++)
                    indexList.Add(i);
            else
                for (int i = 0; i < questionList.Count; i++)
                    indexList.Add(i);

            currentItem = ShuffleValue();
        }

        private int ShuffleValue() {

            int randomIndex = Random.Range(0, indexList.Count);
            int selectedItem = indexList[randomIndex];
            indexList.RemoveAt(randomIndex);

            return selectedItem;
        }


        public void OnResponseSelection() {
            nextButton.interactable = true;
        }

        public void OnNextButton() {

            CsvWrite.instance.responseTime = _timer.ElapsedTimeAndRestart();

            nextButton.interactable = false;

            _csvWriter.item = currentItem;

            if (!setValueOutside)
                _csvWriter.response = ResponseValue();

            _csvWriter.LogTrial();

            _scrollbar.value = 0.5f;

            DoAfterSeletion();
        }


        public float ResponseValue() {

            float currentValue = 0;

                if (!useAnalogueScale) {
                    Toggle[] numberOfToggles = _toggleGroup.GetComponentsInChildren<Toggle>();

                    for (int i = 0; i < numberOfToggles.Length; i++)
                        if (numberOfToggles[i].isOn)
                            currentValue = i;

                    _toggleGroup.SetAllTogglesOff();
                }

                else
                    currentValue = _scrollbar.value;          
            
            return currentValue;
        }

        public void OutsideResponseValue(float outsideValue){
            _csvWriter.response = outsideValue;
        }

        private void DoAfterSeletion(){

            if (!shuffle) {
                currentItem++;

                if (useImages) {
                    if (currentItem < imageList.Count)
                        StartCoroutine(ShowImageForTime());

                    else if (currentItem == imageList.Count)
                        QuestionsExhausted();
                   
                }

                else {
                    if (currentItem < questionList.Count)
                        questionUI.text = questionList[currentItem];

                    else if (currentItem == questionList.Count)
                        QuestionsExhausted();
                }
            }

            else {
                if (indexList.Count != 0) {
                    currentItem = ShuffleValue();

                    if (useImages)
                        StartCoroutine(ShowImageForTime());
                    else {
                        questionUI.text = questionList[currentItem];
                        _timer.stopwatch.Start();
                    }                  
                }

                else if (indexList.Count == 0)
                    QuestionsExhausted();
            }
        }



        private void QuestionsExhausted() {

            _csvWriter.condition++;

            string sceneToLoad = null;
            
            if(LoadSceneAfterTask.instance == null) {
                UnityEngine.Debug.Log("You must attach the LoadSceneAfterTask object somewhere in the scene and add Scene names to it");//else the call is ambiguous for the diagnostics library
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            else {
                if (_csvWriter.condition < ConditionDictionary.selectedOrder.Length)
                    sceneToLoad = LoadSceneAfterTask.instance.beforeLastCondition;

                else if (_csvWriter.condition == ConditionDictionary.selectedOrder.Length)
                    sceneToLoad = LoadSceneAfterTask.instance.afterLastCondition;

                SceneManager.LoadScene(sceneToLoad);
            }
        }

        private IEnumerator ShowImageForTime(){

            allowInput = false;

            _image.gameObject.SetActive(true);
            _image.sprite = imageList[currentItem];

            yield return new WaitForFixedTime(showImageForTime);

            _image.sprite = imageList2[currentItem];

            yield return new WaitForFixedTime(showImageForTime);

            _image.gameObject.SetActive(false);

            _timer.stopwatch.Start();
            allowInput = true;

        }
    }

}
