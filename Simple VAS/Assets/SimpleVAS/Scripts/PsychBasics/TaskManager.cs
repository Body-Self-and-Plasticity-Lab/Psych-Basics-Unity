using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnityPsychBasics {

    public class TaskManager : MonoBehaviour {

        public Text questionUI;
        public Button _nextButton;
        public Scrollbar _scrollbar;
        public ToggleGroup _toggleGroup;
        public Image _image;

        public bool shuffle, useImages, useAnalogueScale;

        [HideInInspector]
        public bool setValueOutside;

        public static TaskManager instance;

        private CsvWrite _csvWriter;
        private CsvRead _csvReader;
        private ImageRead _imageReader;
        private Timer _timer;
        private ScaleSettings _scaleSettings;

        private List<string> questionList = new List<string>();
        private List<Sprite> imageList = new List<Sprite>();
        private List<int> indexList = new List<int>();

        private int currentItem;

        private Vector2 imageProjectionSize;
    
        private void Awake() {

            if (instance == null)
                instance = this;

            _csvWriter = CsvWrite.instance;
            _imageReader = ImageRead.instance;
            _csvReader = CsvRead.instance;
           
        }

        void Start() {

            _nextButton.interactable = false;

            _timer = Timer.instance;
            _timer.stopwatch.Start();

        }

        public void InitializeValuesListsAndObjects() {

            _scaleSettings = ScaleSettings.instance;
           _scaleSettings.CreateToggles();

            if(setValueOutside) {
                _toggleGroup.gameObject.SetActive(false);
                _scrollbar.gameObject.SetActive(false);
                _nextButton.gameObject.SetActive(false);
            }

            else {
                if (useAnalogueScale) {
                    _toggleGroup.gameObject.SetActive(false);
                    _scrollbar.gameObject.SetActive(true);
                }

                else {
                    _scrollbar.gameObject.SetActive(false);
                    _toggleGroup.gameObject.SetActive(true);
                }
            }

            if (useImages) {

                _image.gameObject.SetActive(true);
                questionUI.gameObject.SetActive(false);

                imageProjectionSize = new Vector2(_image.rectTransform.rect.width, _image.rectTransform.rect.height);             
                
                for (int i = 0; i < _imageReader.imageSprites.Count; i++)
                    imageList.Add(_imageReader.imageSprites[i]);

                if (shuffle)
                    CreateShuffleList();

                SetImage();

            }

            else {
                _csvReader.SetFileToLoad();

                _nextButton.interactable = false;
                _image.gameObject.SetActive(false);
                questionUI.gameObject.SetActive(true);

                for (int i = 0; i < _csvReader.questionnaireInput.Count; i++)
                    questionList.Add(_csvReader.questionnaireInput[i]);

                if (shuffle)
                    CreateShuffleList();

                questionUI.text = questionList[currentItem];
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
            _nextButton.interactable = true;
        }

        public void OnNextButton() {

            CsvWrite.instance.responseTime = _timer.ElapsedTimeAndRestart();

            _nextButton.interactable = false;

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
                    _nextButton.interactable = false;
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
                        SetImage();

                    else if (currentItem == imageList.Count)
                        QuestionsExhausted();

                    _timer.stopwatch.Start();
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
                        SetImage();

                    else
                        questionUI.text = questionList[currentItem];

                    _timer.stopwatch.Start();
                }

                else if (indexList.Count == 0)
                    QuestionsExhausted();
            }
        }

        private void SetImage(){
            
            _image.sprite = imageList[currentItem];
            float ratio = (float)_image.sprite.rect.height / (float)_image.sprite.rect.width;
            _image.GetComponent<RectTransform>().sizeDelta = new Vector2(imageProjectionSize.x, imageProjectionSize.y*ratio);            //_image.GetComponent<RectTransform>().rect.height*ratio
        }


        private void QuestionsExhausted() {

            _csvWriter.condition++;
            currentItem = 0;
            questionList.Clear();
            imageList.Clear();
            indexList.Clear();

            _nextButton.interactable = false;

            if (LoadAfterTask.instance == null) {
                UnityEngine.Debug.Log("You must attach the LoadSceneAfterTask object somewhere in the scene and add Scene names to it");//else the call is ambiguous for the diagnostics library
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            else {
                if (_csvWriter.condition < ConditionDictionary.selectedOrder.Length)
                    LoadAfterTask.instance.LoadBeforeLast();

                else if (_csvWriter.condition == ConditionDictionary.selectedOrder.Length)
                    LoadAfterTask.instance.LoadAfterLast();
            }
        }

    }

}
