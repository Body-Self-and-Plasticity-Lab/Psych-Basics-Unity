﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnityPsychBasics {

    public class TaskManager : MonoBehaviour {

        public Text textUI;
        public Button _nextButton;
        public Scrollbar _scrollbar;
        public ToggleGroup _toggleGroup;
        public Image _image;
        public GameObject instructions;

        [HideInInspector]
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
        private ScaleManager _scaleManager;

        private List<string> questionList = new List<string>();

        public List<Sprite> imageList = new List<Sprite>();
        public List<Sprite> imageList2 = new List<Sprite>();

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

        }

        public void InitializeValuesListsAndObjects() {

            _scaleManager = ScaleManager.instance;
            _scaleManager.CreateToggles();

            if(setValueOutside) 
                ShowGameObjects(new GameObject[]{  });
                      
            else {
                if (useAnalogueScale && useImages) 
                    ShowGameObjects(new GameObject[] {_scrollbar.gameObject, _image.gameObject, _nextButton.gameObject});
                else if (!useAnalogueScale && useImages)
                    ShowGameObjects(new GameObject[] { _toggleGroup.gameObject, _image.gameObject, _nextButton.gameObject });
                else if (useAnalogueScale && !useImages)
                    ShowGameObjects(new GameObject[] { _scrollbar.gameObject, textUI.gameObject, _nextButton.gameObject });
                else if (!useAnalogueScale && !useImages)
                    ShowGameObjects(new GameObject[] { _toggleGroup.gameObject, textUI.gameObject, _nextButton.gameObject });
            }

            if (useImages) {

                imageProjectionSize = new Vector2(_image.rectTransform.rect.width, _image.rectTransform.rect.height);             
                
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
                _csvReader.SetFileToLoad();

                for (int i = 0; i < _csvReader.questionnaireInput.Count; i++)
                    questionList.Add(_csvReader.questionnaireInput[i]);

                if (shuffle)
                    CreateShuffleList();

                textUI.text = questionList[currentItem];
            }


        }

        private void ShowGameObjects(GameObject[] objectToShow)
        {
            GameObject[] _gameObjectsToShow = {_toggleGroup.gameObject, _scrollbar.gameObject, _nextButton.gameObject, _image.gameObject, textUI.gameObject};

            foreach (GameObject _object in _gameObjectsToShow) {
                _object.SetActive(false);
                
                for (int i = 0; i < objectToShow.Length; i++) {
                    if (objectToShow[i] == _object)
                        _object.SetActive(true);
                }

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
                        StartCoroutine(ShowImageForTime());

                    else if (currentItem == imageList.Count)
                        QuestionsExhausted();
                   
                }

                else {
                    if (currentItem < questionList.Count)
                        textUI.text = questionList[currentItem];

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
                        textUI.text = questionList[currentItem];
                        _timer.stopwatch.Start();
                    }                  
                }

                else if (indexList.Count == 0)
                    QuestionsExhausted();
            }
        }


        private void QuestionsExhausted() {

            _csvWriter.condition++;
            currentItem = 0;
            questionList.Clear();
            imageList.Clear();
            indexList.Clear();

            _nextButton.interactable = false;
            
            if (TaskSettings.instance == null) {
                UnityEngine.Debug.Log("You must attach the LoadSceneAfterTask object somewhere in the scene and add Scene names to it");//else the call is ambiguous for the diagnostics library
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            else {
                if (_csvWriter.condition < ConditionDictionary.selectedOrder.Length)
                    TaskSettings.instance.LoadBeforeLast();

                else if (_csvWriter.condition == ConditionDictionary.selectedOrder.Length)
                    TaskSettings.instance.LoadAfterLast();
            }
        }

        private IEnumerator ShowImageForTime(){

            allowInput = false;

            _image.gameObject.SetActive(true);
            _image.sprite = imageList[currentItem];

            float ratio = (float)_image.sprite.rect.height / (float)_image.sprite.rect.width;
            _image.GetComponent<RectTransform>().sizeDelta = new Vector2(_image.sprite.rect.width, _image.sprite.rect.height);

            yield return new WaitForFixedTime(showImageForTime);

            _image.sprite = imageList2[currentItem];

            ratio = (float)_image.sprite.rect.height / (float)_image.sprite.rect.width;
            _image.GetComponent<RectTransform>().sizeDelta = new Vector2(_image.sprite.rect.width, _image.sprite.rect.height);

            yield return new WaitForFixedTime(showImageForTime);

            _image.gameObject.SetActive(false);

            _timer.stopwatch.Start();
            allowInput = true;

        }

        public void LoadScene(string scene) {
            SceneManager.LoadScene(scene);
        }

    }

    }
